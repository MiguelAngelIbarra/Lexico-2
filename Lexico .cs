using System.IO;
using System;


namespace Lexico_2
{ 
    /*
        Requerimiento 1: numeros 
        Requerimiento 2: operadores logicos
        Requerimiento 3: operadores relaciones
        Requerimiento 4: operador de termino e incremento de termino 
        Requerimiento 5: Cadena 
        Requerimiento 6: Comentarios
        Requerimiento 7: Excepcion de error
    */
    public class Lexico:Token 
    {
        const int  E=-2;//negativo por si crece el automata 
        const int  F=-1;
        
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
            int sigEstado = EstadoActual;
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
                    else if(char.IsDigit(Trancision))
                    { 
                        sigEstado = 2;
                    }
                    else if(Trancision== '=')
                    { 
                        sigEstado=8;
                    }
                    else if(Trancision== ':')
                    { 
                        sigEstado=10;
                    }
                    else if(Trancision== ';')
                    {
                        sigEstado=12;
                    }

                    else if(Trancision== '*'||Trancision== '%')
                    {
                        sigEstado=27;
                    }
                    else if(Trancision== '?')
                    {
                        sigEstado=32;
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
                    setClasificacion(Tipos.numero);
                    if(char.IsDigit(Trancision))
                    {
                        sigEstado = 2;
                    }
                    else
                    {
                        sigEstado =F;
                    }
                    break;
                case 3: 
                case 4: 
                case 5: 
                case 6: 
                case 7: 
                case 8: 
                    setClasificacion(Tipos.asignacion);
                    if(Trancision=='=')
                    { 
                        sigEstado=9;
                    }
                    else
                    {
                        sigEstado =F;
                    }
            
                    break;
                case 9: 
                    setClasificacion(Tipos.opRelacional);
                    sigEstado=F;
                    break;
                case 10:
                    setClasificacion(Tipos.caracter);
                    if(Trancision=='=')
                    { 
                        sigEstado=11;
                    } 
                    else
                    {
                        sigEstado=F;
                    }
                    break;
                case 11:
                    setClasificacion(Tipos.inicializacion);
                    sigEstado=F;
                    break; 
                case 12:
                    setClasificacion(Tipos.finSentencia); 
                    sigEstado=F;
                    break;
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
                    setClasificacion(Tipos.opFactor);
                    if(Trancision=='=')
                    { 
                        sigEstado=28;
                    }
                    else
                    {
                        sigEstado=F;
                    }
                    break;
                case 28:
                    setClasificacion(Tipos.incFactor);
                    sigEstado=F;
                    break; 
                case 29: 
                case 30: 
                case 31: 
                case 32:
                    setClasificacion(Tipos.ternario);
                    sigEstado=F;
                    break; 
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
            if(Estado==E)
            {
                //levantar excepcion correspondiente
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
