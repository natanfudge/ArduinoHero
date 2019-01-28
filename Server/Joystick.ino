#define JOYSTICK_KEY 2
#define JOYSTICK_X A0
#define JOYATICK_Y A1

void setupJoystick() {
  pinMode(JOYSTICK_KEY, INPUT);
  digitalWrite(JOYSTICK_KEY, HIGH);
}

boolean debugJoystick = true;
void loopJoystick() {
  int x = analogRead(JOYSTICK_X);
  int y = analogRead(JOYATICK_Y);
  boolean buttonPressed = !digitalRead(JOYSTICK_KEY);

  if (debugJoystick) {
    soutC("X = ", x);
    soutC("Y = " , y);
    soutC("Button Pressed = " , buttonPressed);
  }

  if(y < 50){
    onJoystickLeft();
  }

  if(y > 850){
    onJoystickRight();
  }

  if (buttonPressed) {
    onJoystickClicked();
  }
}



void onJoystickClicked(){
  if(debugJoystick){
      sout("Joystick clicked");
  }
}

void onJoystickRight(){
  if(debugJoystick){
      sout("Joystick turned towards right");
  }
}

void onJoystickLeft(){
  if(debugJoystick){
      sout("Joystick turned towards left");
  }
}
