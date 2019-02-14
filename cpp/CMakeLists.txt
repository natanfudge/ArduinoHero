cmake_minimum_required(VERSION 3.10)
project(cpp)

set(CMAKE_CXX_STANDARD 14)

add_executable(cpp
        main.cpp
        pt-1.4/lc.h
        pt-1.4/lc-addrlabels.h
        pt-1.4/lc-switch.h
        pt-1.4/pt.h
        pt-1.4/pt-sem.h
        PROGMEM_readAnything/PROGMEM_readAnything.h
        Util.cpp
        Util.h
        song.cpp
        song.h
        MK_slow_simple_easy.h
        MK_slow_simple_easy.cpp

        orchestra.cpp orchestra.h)

#find_package(pt-1.4)
#find_package(PROGMEM_readAnything)
#target_link_libraries(cpp pt-1.4 PROGMEM_readAnything)