//===============================================
//                  testTouch
//===============================================
byte testTouch(){
byte i;
for(i=0; i<7 ; i++){
 if(x > 20+40*i && x<60+40*i && y>60 && y<60+170)
     return i; 
   }
 return 0xff; 
}//End of testTouch function
//===============================================
//         End Of File "print"        
//===============================================  

