#include <iostream>
//#include <w32api/sapi.h>

#include "Util.h"
#include "pt-1.4/pt.h"
#include <windows.h>
#include "song.h"
#include "MK_slow_simple_easy.h"
#include "orchestra.h"

using namespace std;
static struct pt pt1, pt2; // each protothread needs one of these

static int protothread1(struct pt *pt, int interval);
static int protothread2(struct pt *pt, int interval);
void setupPt();

int main() {

    initSongPt();
    const Note* song = get_MK_orchestra_easy();

    while (millis() < 50000) {
        playSong(song);
    }


    getchar();


     // 523 hertz (C5) for 500 milliseconds
//    cin.get(); // wait





    return 0;
}


void ptTest1() {
    puts("TEST1");
    Beep(423,200);
}

void ptTest2() {
    puts("TEST2");
}




void setupPt() {
    PT_INIT(&pt1);  // initialise the two
    PT_INIT(&pt2);  // protothread variables
}


///* This function toggles the LED after 'interval' ms passed */
//static int protothread1(struct pt *pt, int interval) {
//    async(pt,
//          await(interval);
//                  ptTest1();
//    );
//
//}
//
///* exactly the same as the protothread1 function */
//static int protothread2(struct pt *pt, int interval) {
//    async(pt,
//          await(interval);
//                  ptTest2();
//    );
//}



//void loopTest() {
//    protothread1(&pt1, 2000); // schedule the two protothreads
//    protothread2(&pt2, 1000); // by calling them infinitely
//    Serial.println("meantime");
//}
