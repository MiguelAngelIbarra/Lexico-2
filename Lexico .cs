using System.IO;
using System;


namespace Lexico_2
{ 
    
    public class Lexico:Token 
    {
        
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
        public void NextToken()
        {
            char c;
            string Buffer="";
            
            
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