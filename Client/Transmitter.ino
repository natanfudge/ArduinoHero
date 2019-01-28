#include <SoftwareSerial.h>
SoftwareSerial Transmitter(0, 1);
void setupTransmitter() {
  Transmitter.begin(9600);
}


void loopTransmitter() {
//  Serial.println("omar");
  Transmitter.write(7);
}
//void loopTransmitter() {
//  if (Transmitter.available()) {
//    Serial.println(Transmitter.read());
//  }
//}
