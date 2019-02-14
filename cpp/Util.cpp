//
// Created by natan on 2/14/2019.
//


#include "pt-1.4/pt.h"   // include protothread library
#include <ctime>
#include "Util.h"



unsigned long millis() {
    using namespace std;
    static clock_t begin = clock();

//    code_to_time();

    clock_t end = clock();
    double elapsed_mils = double(end - begin) / CLOCKS_PER_SEC * 1000;
    return (unsigned long)elapsed_mils;
}

void block(int timeMills){
    unsigned long startTime = millis();
    unsigned long currentTime = 0;

//    = millis();

    while(currentTime < startTime + timeMills){
        currentTime = millis();
    }
}