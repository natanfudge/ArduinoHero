//
// Created by natan on 2/14/2019.
//

#ifndef CPP_SONG_H
#define CPP_SONG_H
typedef struct {
    unsigned long time : 12;
    unsigned int col : 2;
}Note;
void initSongPt();
 int playSong(const Note* song);
#endif //CPP_SONG_H
