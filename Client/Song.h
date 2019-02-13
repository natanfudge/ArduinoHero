//
// Created by natan on 12/19/2018.
//

#pragma once


#define SEGMENT_SIZE 50

typedef struct {
    unsigned long time : 12;
    unsigned int col : 2;
}Note;

typedef struct{
    Note notes[SEGMENT_SIZE];
    int length;
}SongSegment;




class Song {
protected:
    const  SongSegment currentSegment;
    int segmentIndex = 0;
    int noteIndex = 0;
public:
    virtual void advanceNote() = 0;
    Note getCurrentNote();
    virtual bool hasNext() = 0;
};
