using System.IO;
using System;


namespace Lexico_2
{ 
    
    public class Lexico:Token 
    {
        const int  E=-1;//negativo por si crece el automata 
        const int  F=-2;
        
        StreamReader archivo;
        StreamWriter log;
        public Lexico()
        {
            archivo = new StreamReader("D:\\Archivos\\Hola.txt");
            log = new StreamWriter("D:\\Archivos\\Log.txt");
            log.AutoFlush=true;
            log.WriteLine("Instituo Tecnologico de Queretaro");
            log.WriteLine("Miguel Angel Ibarra Basaldua");
            log.WriteLine("-----------------------------------");
            log.WriteLine("Contenido de Hola.txt: ");
        }
        public int Automata(int EstadoActual,char Trancision)
        {
            int sigEstado = 0;
            switch(EstadoActual)
            { 
                case 0:
                    if(char.IsWhiteSpace(Trancision))
                    { 
                        sigEstado = 0;
                    }
                    else if(char.IsLetter(Trancision))
                    { 
                        sigEstado=1;
                    }
                    else
                    {
                        sigEstado=33;
                    }
                    break;
                case 1:
                    setClasificacion(Tipos.identificador);
                    if(!char.IsLetterOrDigit(Trancision))
                    {
                        sigEstado = F;
                    }
                    break;

                case 2: 
                case 3: 
                case 4: 
                case 5: 
                case 6: 
                case 7: 
                case 8: 
                case 9: 
                case 10: 
                case 11: 
                case 12: 
                case 13: 
                case 14: 
                case 15: 
                case 16: 
                case 17: 
                case 18: 
                case 19: 
                case 20: 
                case 21: 
                case 22: 
                case 23: 
                case 24: 
                case 25: 
                case 26: 
                case 27: 
                case 28: 
                case 29: 
                case 30: 
                case 31: 
                case 32: 
                case 33:
                    setClasificacion(Tipos.caracter);
                    sigEstado=F;
                    break;
                case 34: 
                case 35: 
                case 36: 
                case 37: 
                case 38: 
                case 39: 
                break;

            }



            return sigEstado;
        }
        public void NextToken()
        {
            char c;
            string Buffer="";
            int Estado=0;

            while(Estado>=0)
            {
                c=(char)archivo.Peek();
                Estado=Automata(Estado,c);
                if(Estado>=0)
                {
                    archivo.Read();
                    if(Estado>0)
                    { 
                        Buffer+=c;
                    }
                }

            }

            
            
            setContenido(Buffer);
            if(getClasificacion()==Tipos.identificador)
            {
                switch(getContenido())
                { 
                    case "char":
                    case "int": 
                    case "float":
                    setClasificacion(Tipos.tipoDato);
                    break;
                    //zona 
                    case "private":
                    case "public": 
                    case "protected":
                    setClasificacion(Tipos.zona);
                    break;
                    //condicion
                    case "if":
                    case "else": 
                    case "switch":
                    setClasificacion(Tipos.condicion);
                    break;
                    //ciclos
                    case "while":
                    case "for": 
                    case "do":
                    setClasificacion(Tipos.ciclo);
                    break;
                }
            }
            if(!FinAchivo())
            {
                log.WriteLine(getContenido()+" = "+getClasificacion());
            }
        }
        public bool FinAchivo()
        {
            return archivo.EndOfStream;
        }

    }
}
