#define WIDTH 320
#define HEIGHT 240

#define FONT_SIZE 3
#define X 50
#define Y 50

#define LENGTH 100
#define VERTICAL_X_START 100
#define VERTICAL_Y_START 100

#define HORIZONTAL_X_START 100
#define HORIZONTAL_Y_START 100

#define CORNER_X 150
#define CORNER_Y 120
#define RECT_WIDTH 40
#define RECT_HEIGHT 60

#define SCORE_1_X (WIDTH * (2.0 / 3.8))
#define SCORE_1_Y (HEIGHT / 2 - 20)

#define SCORE_2_X (WIDTH * (1.0/3.8))
#define SCORE_2_Y (HEIGHT / 2 - 20)

#define SCORE_FONT 3


#include "SPI.h"
#include "TFT9341.h"
#include "touch.h"
void setupScreen() {
  lcd.begin();
  lcdtouch.begin();
}

int eldar = 5599;

void setScore1(int score) {
  lcd.gotoxy(SCORE_1_X, SCORE_1_Y);
  lcd.setFont(SCORE_FONT);
  lcd.setColor(BLACK);
  lcd.print(score);
}

void setScore2(int score) {
  lcd.gotoxy(SCORE_2_X, SCORE_2_Y);
  lcd.setFont(SCORE_FONT);
  lcd.setColor(BLACK);
  lcd.print(score);
}

void loopScreen() {
  //  lcd.gotoxy(50, 50);
  //  lcd.setFont(5);
  //  lcd.print("AMAR");
  //
  //  lcd.drawVLine(WIDTH - 50, HEIGHT - 100, 100);
  //  lcd.drawHLine(WIDTH - 100, HEIGHT, LENGTH);
  //
  //  lcd.fillRect(CORNER_X, CORNER_Y, RECT_WIDTH, RECT_HEIGHT, GREEN);

  setScore1(eldar);
  setScore2(322);


}

