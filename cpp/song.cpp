//
// Created by natan on 2/14/2019.
//

#include "song.h"
#include "Util.h"
#include "pt-1.4/pt.h"
#include <windows.h>

struct pt songPt;

void initSongPt(){
    PT_INIT(&songPt);
}

void beep(){
    Beep(423,200);
}

int time_to_wait(Note note){
    int time = note.time;
    return time / 2;
}

 int playSong(const Note* song){
    static int note = 0;

    //TODO add note amount
   async(&songPt,
          await(&songPt,time_to_wait(song[note]));
   note++;
                  beep();
    );
}

