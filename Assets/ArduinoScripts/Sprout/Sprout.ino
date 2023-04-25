#include "DHT.h" //DHT Bibliothek laden
#define DHTPIN 2 //Der Sensor wird an PIN 2 angeschlossen    
#define DHTTYPE DHT11    // Es handelt sich um den DHT11 Sensor
DHT dht(DHTPIN, DHTTYPE); //Der Sensor wird ab jetzt mit „dth“ angesprochen

int soilhum = 0;

int incomingByte;
int redPin= 9;
int greenPin = 10;
int bluePin = 11;

int rValue = 0;
int gValue = 0;
int bValue = 0;

void setup()
{
  //Set up RGB LED
  pinMode(redPin, OUTPUT);
  pinMode(greenPin, OUTPUT);
  pinMode(bluePin, OUTPUT);

  //
  Serial.begin(9600);
  dht.begin(); //DHT11 Sensor starten
}

void loop()
{
  //Soil humidity
  soilhum = analogRead(A0);
  Serial.print("soilhum:");
  Serial.println(soilhum);

  //Temperature and air humidity
  delay(1000); //Zwei Sekunden Vorlaufzeit bis zur Messung (der Sensor ist etwas träge)
  
  float Luftfeuchtigkeit = dht.readHumidity(); //die Luftfeuchtigkeit auslesen und unter „Luftfeutchtigkeit“ speichern
  float Temperatur = dht.readTemperature();//die Temperatur auslesen und unter „Temperatur“ speichern
  
  Serial.print("airhum:"); //Im seriellen Monitor den Text und 
  Serial.println(Luftfeuchtigkeit); //die Dazugehörigen Werte anzeigen
  Serial.print("airtmp:");
  Serial.println(Temperatur);

  //RGB LED
    while(Serial.available() > 0)
    {
      incomingByte = Serial.read();

      if(incomingByte == 'R')
      {
        rValue = Serial.parseInt();
      }

      if(incomingByte == 'G')
      {
        gValue = Serial.parseInt();
      }

      if(incomingByte == 'B')
      {
        bValue = Serial.parseInt();
      }
  }

  //Set LED color
  setColor(rValue, gValue, bValue);
}

void setColor(int redValue, int greenValue, int blueValue)
{
  analogWrite(redPin, redValue);
  analogWrite(greenPin, greenValue);
  analogWrite(bluePin, blueValue);
}
