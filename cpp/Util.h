//
// Created by natan on 2/14/2019.
//

#ifndef CPP_UTIL_H
#define CPP_UTIL_H
unsigned long millis();
void block(int time);
#define async(pt,code) static unsigned long timestamp = 0;\
PT_BEGIN(pt);\
while(1) {  \
code\
}\
PT_END(pt);\

#define await(pt,time)PT_WAIT_UNTIL(pt, millis() - timestamp > time );\
timestamp = millis();
#endif //CPP_UTIL_H
