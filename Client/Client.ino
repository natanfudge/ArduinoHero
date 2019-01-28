#include <Event.h>
#include <Timer.h>
#include "Song.h"

#include "Megalovania.cpp"
const long x[2000] = {2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 11, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1};




void setup() {

  Serial.begin(9600);
  setupLeds();
  setupButtons();
  setupScreen();
  setupTransmitter();
  setupTest();


  int y = x[0];



  //  Megalovania s1;
  //  Megalovania * p = &s1;
  //
  //     while(s1.hasNext()){
  //        Note n = s1.getCurrentNote();
  //        Serial.println("Time = ");
  //        s1.advanceNote();
  //    }
  //  playSong(s1);
  //    delete s1;

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


const SongSegment temp = {
  {
    {0, 2},
    {24, 2},
    {48, 3},
    {96, 2},
    {168, 2},
    {216, 2},
    {264, 2},
    {312, 1},
    {336, 2},
    {360, 2},
    {384, 0},
    {408, 0},
    {432, 3},
    {480, 2},
    {552, 2},
    {600, 2},
    {648, 2},
    {696, 1},
    {720, 2},
    {744, 2},
    {768, 0},
    {792, 0},
    {816, 3},
    {864, 2},
    {936, 2},
    {984, 2},
    {1032, 2},
    {1080, 1},
    {1104, 2},
    {1128, 2},
    {1152, 0},
    {1176, 0},
    {1200, 3},
    {1248, 2},
    {1320, 2},
    {1368, 2},
    {1416, 2},
    {1464, 1},
    {1488, 2},
    {1512, 2},
    {1536, 1},
    {1536, 0},
    {1560, 2},
    {1584, 3},
    {1584, 0},
    {1632, 3},
    {1632, 0},
    {1656, 0},
    {1704, 3},
    {1704, 0}
  },
  50
};
