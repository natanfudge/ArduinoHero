#include "Heap.h"

// A utility function to swap two elements 
void swap(LedSwitch *x, LedSwitch *y) 
{ 
    LedSwitch temp = *x; 
    *x = *y; 
    *y = temp; 
} 

// Constructor: Builds a heap from a given array a[] of given size 
MinHeap::MinHeap(int cap) 
{ 
    heap_size = 0; 
    capacity = cap; 
    harr = new LedSwitch[cap]; 
} 
  
// Inserts a new key 'k' 
void MinHeap::insertKey(LedSwitch k) 
{ 
    if (heap_size == capacity) 
    { 
//        Serial.println("\nOverflow: Could not insertKey\n"); 
        return; 
    } 
  
    // First insert the new key at the end 
    heap_size++; 
    int i = heap_size - 1; 
    harr[i] = k; 
  
    // Fix the min heap property if it is violated 
    while (i != 0 && harr[parent(i)].time > harr[i].time) 
    { 
       swap(&harr[i], &harr[parent(i)]); 
       i = parent(i); 
    } 
} 
  
  
// Method to remove minimum element (or root) from min heap 
LedSwitch MinHeap::extractMin() 
{ 
    if (heap_size <= 0) 
        return {-1,-1}; 
    if (heap_size == 1) 
    { 
        heap_size--; 
        return harr[0]; 
    } 
  
    // Store the minimum value, and remove it from heap 
    LedSwitch root = harr[0]; 
    harr[0] = harr[heap_size-1]; 
    heap_size--; 
    MinHeapify(0); 
  
    return root; 
} 
  
  // A recursive method to heapify a subtree with the root at given index 
// This method assumes that the subtrees are already heapified 
void MinHeap::MinHeapify(int i) 
{ 
    int l = left(i); 
    int r = right(i); 
    int smallest = i; 
    if (l < heap_size && harr[l].time < harr[i].time) 
        smallest = l; 
    if (r < heap_size && harr[r].time < harr[smallest].time) 
        smallest = r; 
    if (smallest != i) 
    { 
        swap(&harr[i], &harr[smallest]); 
        MinHeapify(smallest); 
    } 
} 

////// This function deletes key at index i. It first reduced value to minus 
////// infinite, then calls extractMin() 
//void MinHeap::deleteKey(LedSwitch i) 
//{ 
//    decreaseKey(i, INT_MIN); 
//    extractMin(); 
//} 
//  
  

