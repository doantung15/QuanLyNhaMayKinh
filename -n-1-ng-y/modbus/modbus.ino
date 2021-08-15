#include <Modbus.h>
#include <ModbusSerial.h>
ModbusSerial mb;
int a;
void setup() {
  // put your setup code here, to run once:
  mb.config(&Serial1,115200 , SERIAL_8N1);
  mb.setSlaveId(30);
  DDRA=0XFF;
  DDRB=0X70;
  DDRC=0X03;
  DDRD=0X80;
  DDRE=0X38;
  DDRF=0XFF;
  DDRG=0X07;
  DDRH=0X78;  
  DDRK=0XFF;
  DDRL=0XFF;     
  for(int i=0; i<20;i++)
   {
      mb.addHreg(i);
   }
  for(int j=0;j<20;j++)
  {
    mb.addCoil(j);
  }
}

void loop() {
  // put your main code here, to run repeatedly:
  mb.task();
  mb.Coil(0,1);
  mb.Hreg(10,a);
  a++;
  
  
  PORTA= mb.Hreg(0);
  PORTB=mb.Hreg(1);
  PORTC= mb.Hreg(2);
  PORTD=mb.Hreg(3);
  PORTE=mb.Hreg(4);
  PORTF=mb.Hreg(5);
  PORTG=mb.Hreg(6);
  PORTH=mb.Hreg(7);
  PORTK=mb.Hreg(8);
  PORTL=mb.Hreg(9);
}
