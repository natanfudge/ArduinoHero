#include <pt.h>   // include protothread library

#define LEDPIN 13  // LEDPIN is a constant 
#define async(pt,code) static unsigned long timestamp = 0;\
PT_BEGIN(pt);\
  while(1) {  \
   code\
  }\
  PT_END(pt);\

#define await(time) PT_WAIT_UNTIL(pt, millis() - timestamp > time );\
  timestamp = millis();\




static struct pt pt1, pt2; // each protothread needs one of these

void setupTest() {
  pinMode(LEDPIN, OUTPUT); // LED init
  PT_INIT(&pt1);  // initialise the two
  PT_INIT(&pt2);  // protothread variables
}

//void toggleLED() {
//  Serial.println("TOGGLED");
////  boolean ledstate = digitalRead(LEDPIN); // get LED state
////  ledstate ^= 1;   // toggle LED state using xor
////  digitalWrite(LEDPIN, ledstate); // write inversed state back
//}

void ptTest1(){
  Serial.println("TEST1");
  
}

void ptTest2(){
  Serial.println("TEST2");
}

/* This function toggles the LED after 'interval' ms passed */
static int protothread1(struct pt *pt, int interval) {
  async(pt,
    PT_WAIT_UNTIL(pt, millis() - timestamp > interval);
    timestamp = millis(); // take a new timestamp
    ptTest1();
  );
 
}
/* exactly the same as the protothread1 function */
static int protothread2(struct pt *pt, int interval) {
  async(pt,
    await(interval);
    ptTest2();
  );
}



void loopTest() {
  protothread1(&pt1, 2000); // schedule the two protothreads
  protothread2(&pt2, 1000); // by calling them infinitely
  Serial.println("meantime");
}
