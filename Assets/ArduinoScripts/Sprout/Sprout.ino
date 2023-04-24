#include "DHT.h" //DHT Bibliothek laden
#define DHTPIN 2 //Der Sensor wird an PIN 2 angeschlossen    
#define DHTTYPE DHT11    // Es handelt sich um den DHT11 Sensor
DHT dht(DHTPIN, DHTTYPE); //Der Sensor wird ab jetzt mit „dth“ angesprochen

int soilhum = 0;

void setup()
{
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
  delay(2000); //Zwei Sekunden Vorlaufzeit bis zur Messung (der Sensor ist etwas träge)
  
  float Luftfeuchtigkeit = dht.readHumidity(); //die Luftfeuchtigkeit auslesen und unter „Luftfeutchtigkeit“ speichern
  float Temperatur = dht.readTemperature();//die Temperatur auslesen und unter „Temperatur“ speichern
  
  Serial.print("airhum:"); //Im seriellen Monitor den Text und 
  Serial.println(Luftfeuchtigkeit); //die Dazugehörigen Werte anzeigen
  Serial.print("airtmp:");
  Serial.println(Temperatur);
}
