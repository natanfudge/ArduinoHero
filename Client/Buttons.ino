#define BUTTON_1 2
#define BUTTON_2 3
#define BUTTON_3 4
#define BUTTON_4 5
#define CLICK_BUFFER_TIME 100

#define TEST_BUTTONS false
#define TEST_PRESS true
#define TEST_TIME_BUFFER false

void setupButtons() {
  pinMode(BUTTON_1, INPUT_PULLUP);
  pinMode(BUTTON_2, INPUT_PULLUP);
  pinMode(BUTTON_3, INPUT_PULLUP);
  pinMode(BUTTON_4, INPUT_PULLUP);

}

// Indicator if the player is holding a certain button. Used to make it so when you click the functions are only called once until you repress the buttons.
bool holding1 = false,holding2 = false, holding3 = false, holding4 = false;
long pressTime1 = 0, pressTime2 = 0, pressTime3 = 0, pressTime4 = 0;

void loopButtons() {
  bool pressed1 = !digitalRead(BUTTON_1);
  bool pressed2 = !digitalRead(BUTTON_2);
  bool pressed3 = !digitalRead(BUTTON_3);
  bool pressed4 = !digitalRead(BUTTON_4);

  if (TEST_BUTTONS) {
    soutInt("Button 1 = ", pressed1);
    soutInt("Button 2 = ", pressed2);
    soutInt("Button 3 = ", pressed3);
    soutInt("Button 4 = ", pressed4);
    delay(1000);
  }

  checkButtonPress(pressed1, &holding1, &pressTime1, onFirstPressed);
  checkButtonPress(pressed2, &holding2, &pressTime2, onSecondPressed);
  checkButtonPress(pressed3, &holding3,  &pressTime3, onThirdPressed);
  checkButtonPress(pressed4, &holding4,  &pressTime4, onFourthPressed);


}

void checkButtonPress(bool buttonPressed, bool* holding, long* pressTime, void (*func)()) {
  if (buttonPressed) {
    if(TEST_TIME_BUFFER){
      if(!*holding){
        Serial.println("No buffer press");
        Serial.print("Press time = ");
        Serial.println(*pressTime);

      soutInt("Time passed =", millis() - *pressTime); 
      }
      
    }
    if (!(*holding)) {
      *pressTime = millis();

      (*func)();
    }
    *holding = true;
  } else if(millis() - *pressTime > CLICK_BUFFER_TIME){
    *holding = false;
  }
}

void onFirstPressed() {
  if (TEST_PRESS) {
    sout("PRESSED 1");
  }
}

void onSecondPressed() {
  if (TEST_PRESS) {
    sout("PRESSED 2");
  }
}

void onThirdPressed() {
  if (TEST_PRESS) {
    sout("PRESSED 3");
  }
}

void onFourthPressed() {
  if (TEST_PRESS) {
    sout("PRESSED 4");
  }
}

