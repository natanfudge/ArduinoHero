#include <Event.h>
#include <Timer.h>
#include "Song.h"
#include <PROGMEM_readAnything.h>





void setup() {

  Serial.begin(9600);
  setupLeds();
  setupButtons();
  setupScreen();
  setupTransmitter();
  setupTest();

  Note* notes = get_Megalovania_easy();

  for (int i = 0; i <5000; i++)
  {
    Note note;
    PROGMEM_readAnything (&notes [i], note);
    Serial.println (note.time);
   
  }  // en


}



void loop() {
  loopButtons();
  loopLeds();
  loopScreen();
  loopTransmitter();
  loopTest();

}

void sout(String str) {
  Serial.println(str);
}
