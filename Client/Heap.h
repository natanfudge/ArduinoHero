#include <FastLED.h>

typedef struct{
  long time;
  unsigned char pos;
  CRGB color;
}LedSwitch;

//void swap(LedSwitch *x, LedSwitch *y);
//#include "Song.h"



// A class for Min Heap
class MinHeap
{
    LedSwitch *harr; // pointer to array of elements in heap
    int capacity; // maximum possible size of min heap
    int heap_size; // Current number of elements in min heap
  public:
    //    // Constructor
        MinHeap(int capacity);
    //
    //    // to heapify a subtree with the root at given index
        void MinHeapify(int );
    //
        int parent(int i) { return (i-1)/2; }
    //
    //    // to get index of left child of node at index i
        int left(int i) { return (2*i + 1); }
    //
    //    // to get index of right child of node at index i
        int right(int i) { return (2*i + 2); }
    //
    // to extract the root which is the minimum element
    LedSwitch extractMin();

    bool isEmpty(){return heap_size == 0;}
    //
    //    // Decreases key value of key at index i to new_val
//        void decreaseKey(int i, int new_val);

    // Returns the minimum key (key at root) from min heap
    LedSwitch getMin() {
      return harr[0];
    }

    // Deletes a key stored at index i
//        void deleteKey(int i);

    // Inserts a new key 'k'
    void insertKey(LedSwitch k);
};
