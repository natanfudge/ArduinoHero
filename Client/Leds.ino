#include "Heap.h"

#define LED_PIN     6
#define NUM_LEDS    60
#define COL_LENGTH 15
#define COL_1_START  0
#define COL_2_START  29
#define COL_3_START  30
#define COL_4_START  59
#define WAVE_L2L_DELAY 200
#define DELAY_MULTIPLIER 1

#define COL_1_RED 0
#define COL_1_GREEN 0
#define COL_1_BLUE 255
#define COL_2_RED 0
#define COL_2_GREEN 255
#define COL_2_BLUE 0
#define COL_3_RED 255
#define COL_3_GREEN 0
#define COL_3_BLUE 0
#define COL_4_RED 255
#define COL_4_GREEN 255
#define COL_4_BLUE 0

using namespace std;

MinHeap heap(50);

CRGB leds[NUM_LEDS];

Timer t;

void setupLeds() {
  FastLED.addLeds<WS2812, LED_PIN, GRB>(leds, NUM_LEDS);
}

int noteIndex = 0;
int timeSinceStart = 0;
boolean singing = false;
Song *currentSong;

void playSong(Song *song) {
  noteIndex = 0;
  timeSinceStart = millis();
  singing = true;
  currentSong = song;
}

//LedSwitch

void dumpHeap(){
  while(!heap.isEmpty()){
    LedSwitch led = heap.extractMin();
    Serial.print("Time = ");
    Serial.println(led.time);
    Serial.print("Pos = ");
    Serial.println(led.pos);
  }
}

#define DEBUG_CHECK_LED false

void checkLed() {
  if (!heap.isEmpty()) {
    LedSwitch led = heap.getMin();
    if (led.time <= millis()) {

      if(DEBUG_CHECK_LED){
        Serial.print("ACTIVATE! pos = ");
      Serial.println(led.pos);
      Serial.print("Time = ");
      Serial.println(millis());
      }
      

      leds[led.pos] = led.color;
      FastLED.show();

      heap.extractMin();
      // Recursively call to be able to activate multiple leds at once


      checkLed();
    }
  }


}

#define DEBUG_SONG_WAVE_ACTIVATION false

void loopLeds() {
  //  t.update();

 // bool show = !heap.isEmpty() && heap.getMin().time <= millis();

  checkLed();
//  if (show){
//    Serial.println("Show!");
//    FastLED.show();
//  }

if (singing) {
  if (!currentSong->hasNext()) {
      Serial.println("Song done!");
    singing = false;
    return;
  }
  long currentTime = millis();
  Note note = currentSong->getCurrentNote();

    if(DEBUG_SONG_WAVE_ACTIVATION){

//  soutInt("Requested Time = ", note.time * DELAY_MULTIPLIER);
//    soutInt("Current Time = ", currentTime - timeSinceStart);
    }
  if (note.time * DELAY_MULTIPLIER <= currentTime - timeSinceStart) {
    if(DEBUG_SONG_WAVE_ACTIVATION){
      soutInt("Waving after time passed = ", currentTime - timeSinceStart);
    }
    wave(note.col);
    currentSong->advanceNote();
  }

}
}

void soutInt(char message[], long arg){
  Serial.print(message);
  Serial.println(arg);
}


  



void test() {
  Serial.println("Easy Test");
}

int calcPos(int col, int pos) {
  int totalPos;

  switch (col) {
    case 1:
      totalPos = pos;
      break;
    case 2:
      totalPos = COL_2_START - pos;
      break;
    case 3:
      totalPos = COL_3_START  + pos;
      break;
    case 4:
      totalPos = COL_4_START - pos;
      break;
    default:
      totalPos = -1;
      break;
  }
  return totalPos;
}

//Note ledQueue[50];
//int count;



void deactivate(int col, int pos, int timeFromNow) {
  activate(col, pos, 0, 0, 0, timeFromNow);
}

void deactivateNow(int col, int pos) {
  deactivate(col, pos, 0);
}

void activate(int col, int pos, int red, int green, int blue, int timeFromNow) {
  LedSwitch led = {millis() + timeFromNow, calcPos(col, pos), CRGB(red, green, blue)};
  heap.insertKey(led);
}

//void activateNow(int col, int pos, int gree color) {
//  activate(col, pos, color, 0);
//}



void testAmar(void* x) {
  Serial.print((int)x);
}

void wave(int col) {
  for (int i = 0; i < COL_LENGTH; ++i) {
    CRGB color;
    switch (col) {
      case 1:
        color = CRGB(COL_1_GREEN, COL_1_RED, COL_1_BLUE);
        break;
      case 2:
        color = CRGB(COL_2_GREEN, COL_2_RED, COL_2_BLUE);

        break;
      case 3:
        color = CRGB(COL_3_GREEN, COL_3_RED, COL_3_BLUE);

        break;
      case 4:
        color = CRGB(COL_4_GREEN, COL_4_RED, COL_4_BLUE);

        break;
      default:
        Serial.println("Invalid column");
        return;
    }

    activate(col, i, color.red, color.green, color.blue, i * WAVE_L2L_DELAY);
    deactivate(col, i, (i + 1) * WAVE_L2L_DELAY);


  }
}

//
//  void wave(void* params) {
//    // Convert params to column and index
//    int col = (int)params / 100;
//    int i = (int)params % 100;
//
//    int red = 0, green = 0, blue = 0;
//
//    // Choose color
//    switch (col) {
//      case 1:
//        red = COL_1_RED;
//        green = COL_1_GREEN;
//        blue =  COL_1_BLUE;
//        break;
//      case 2:
//        red = COL_2_RED;
//        green = COL_2_GREEN;
//        blue =  COL_2_BLUE;
//        break;
//      case 3:
//        red = COL_3_RED;
//        green = COL_3_GREEN;
//        blue =  COL_3_BLUE;
//        break;
//      case 4:
//        red = COL_4_RED;
//        green = COL_4_GREEN;
//        blue =  COL_4_BLUE;
//        break;
//      default:
//        Serial.println("Invalid column");
//        return;
//    }
//
//    // Deactivate previous led
//    if (i >= 1) {
//      deactivate(col, i - 1);
//
//    }
//    // Activate this led
//    if (i < COL_LENGTH) {
//      activate(col, i , red , green, blue);
//    }
//
//    // Do the same thing after a delay to the next led in the row
//    if (i <= COL_LENGTH) {
//      t.after(WAVE_L2L_DELAY, wave, (void*)(col * 100 + i + 1));
//    }
//  }
//
//  void wave(int col) {
//    wave((void*)(col * 100 ));
//
//  }
