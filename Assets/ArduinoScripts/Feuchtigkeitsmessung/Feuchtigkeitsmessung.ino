int messwert=0; //Unter der Variablen "messwert" wird später der Messwert des Sensors gespeichert.

void setup() //Hier beginnt das Setup.

{

  Serial.begin(9600); //Die Kommunikation mit dem seriellen Port wird gestartet. Das benötigt man, um sich den ausgelesenen Wert im "serial monitor" anzeigen zu lassen.

}

void loop() // Hier beginnt der Hauptteil

{

  messwert=analogRead(A0); //Die Spannung an dem Sensor wird ausgelesen und unter der Variable „messwert“ gespeichert.
  Serial.print("soilhum:"); //Ausgabe am Serial-Monitor: Das Wort „Feuchtigkeits-Messwert:" 
  Serial.println(messwert); //und im Anschluss der eigentliche Messwert.
  delay(500); // Zum Schluss noch eine kleine Pause, damit nicht zu viele Zahlenwerte über den Serial-Monitor rauschen.
}