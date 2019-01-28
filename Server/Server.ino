

void setup() {
    setupJoystick();
  Serial.begin(9600);
}

void loop(){
  loopJoystick();

delay(500);



}



void sout(String str){
  Serial.println(str);
}

void soutC(String str, int num){
  Serial.print(str);
  Serial.println(num);
}
