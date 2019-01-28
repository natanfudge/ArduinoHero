//
// Created by natan on 12/19/2018.
//

#include "Song.h"

Note Song::getCurrentNote(){
    return currentSegment.notes[noteIndex];
}
