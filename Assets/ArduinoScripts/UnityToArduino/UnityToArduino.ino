int incomingByte[2];
int ledPin = 13;
bool led = false;

void setup()
{
  pinMode(ledPin, OUTPUT);
  digitalWrite(ledPin, LOW);
  Serial.begin(9600);

}

void loop()
{
  if(Serial.available() > 0)
  {
    while(Serial.peek() == 'L')
    {
      Serial.read();
      incomingByte[0] = Serial.parseInt();
      if(incomingByte[0] == 1) { led = true; } else { led = false; }
    }

    while(Serial.available() > 0)
    {
      Serial.read();
    }
  }

  ledCheck();
}

void ledCheck()
{
  if(led)
  {
    digitalWrite(ledPin, HIGH);
  }
  else
  {
    digitalWrite(ledPin, LOW);
  }

  return;
}