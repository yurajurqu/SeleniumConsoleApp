using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace SeleniumCM.Models
{
    class AssignationFactory
    {
        public static decimal TIME_PBL_COMPILAR = Decimal.Parse(ConfigurationManager.AppSettings["defaultTimeCompilePBLMin"])/60 ;
        public static decimal DEFAULT_TIME_SEND_MAIL = Math.Round(Decimal.Parse(ConfigurationManager.AppSettings["defaultSendMailTimeMin"]) / 60,2);
        public static decimal DEFECT_WEIGHT = Decimal.Parse(ConfigurationManager.AppSettings["weightDefects"]) ;
        public static decimal WEIGHT_1 = Decimal.Parse(ConfigurationManager.AppSettings["weight1"]);
        

        //t19 DOCUMENTAR REGISTRAR DEFECTOS variable
        //t2 CORREO INICIO ATENCION  5min
        //t16 CORREO INFORMAR INSTALACION  5min
        //t20 CORREO INFORMAR FIN ATENCION   5min
        //t3 CORREO VALIDAR DATOS PRODUCCION solo ciclo 1  5min
        //t8 VALIDAR INSTALACION CICLO PREVIO  solo para ciclos no 1  variable 
        //t1  VALIDAR FUENTES POR LB  
        //t4 DESCARGAR FUENTES variable
        //t5 DESCARGAR FUENTES VERSION PREVIA variable
        //t6 DESCARGAR FUENTES VERSION PREVIA OP solo sga  variable
        //t7 COMPARAR FUENTES  variable
        //t9 COMPILAR FUENTES  seCompilo  variable
        //t10 SUBIR COMPILADOS RTC seSubioEjecutables variable
        //t11 DESCARGAR COMPILADOS RTC seSubioEjecutables  variable
        //t14 CONFIGURAR DDS   variable
        //t13 INSTALACION DE COMPILADOS   variable
        //t12 VALIDAR AMBIENTE PRE INSTALACIÓN  varible
        //t15 VALIDAR AMBIENTE POST INSTALACIÓN   variable
        //t17 ELABORAR EVIDENCIAS CM  variable 
        //t18 VALIDAR MDI CHECKLIST   variable 
        public static Assignation createAssignation(
            AssignationRequest request)
        {
            var assignation = new Assignation();
            assignation.Tasks = new List<Task>();


            var t1 = new Task();
            t1.TaskName = "VALIDAR FUENTES POR LB";
            var t2 = new Task();
            t2.TaskName = "CORREO INICIO ATENCION";
            var t3 = new Task();
            t3.TaskName = "CORREO VALIDAR DATOS PRODUCCION";
            var t4 = new Task();
            t4.TaskName = "DESCARGAR FUENTES";
            var t5 = new Task();
            t5.TaskName = "DESCARGAR FUENTES VERSION PREVIA";
            var t6 = new Task();
            t6.TaskName = "DESCARGAR FUENTES VERSION PREVIA OP solo sga";
            var t7 = new Task();
            t7.TaskName = "COMPARAR FUENTES";
            var t8 = new Task();
            t8.TaskName = "VALIDAR INSTALACION CICLO PREVIO  solo para ciclos no 1";
            var t9 = new Task();
            t9.TaskName = "COMPILAR FUENTES";
            var t10 = new Task();
            t10.TaskName = "SUBIR COMPILADOS RTC";
            var t11 = new Task();
            t11.TaskName = "DESCARGAR COMPILADOS RTC";
            var t12 = new Task();
            t12.TaskName = "VALIDAR AMBIENTE PRE INSTALACIÓN";
            var t13 = new Task();
            t13.TaskName = "INSTALACION DE COMPILADOS";
            var t14 = new Task();
            t14.TaskName = "CONFIGURAR DDS";
            var t15 = new Task();
            t15.TaskName = "VALIDAR AMBIENTE POST INSTALACIÓN";
            var t16 = new Task();
            t16.TaskName = "CORREO INFORMAR INSTALACION";
            var t17 = new Task();
            t17.TaskName = "ELABORAR EVIDENCIAS CM";
            var t18 = new Task();
            t18.TaskName = "VALIDAR MDI CHECKLIST";
            var t19 = new Task();
            t19.TaskName = "DOCUMENTAR REGISTRAR DEFECTOS";
            var t20 = new Task();
            t20.TaskName = "CORREO INFORMAR FIN ATENCION";


            //t1  VALIDAR FUENTES POR LB  
            t1.Weight = WEIGHT_1;
            //t4 DESCARGAR FUENTES variable
            //t5 DESCARGAR FUENTES VERSION PREVIA variable
            t4.Weight = WEIGHT_1;
            t5.Weight = WEIGHT_1;


            //correos constantes
            t2.ReportTime = DEFAULT_TIME_SEND_MAIL;
            t16.ReportTime = DEFAULT_TIME_SEND_MAIL;
            t20.ReportTime = DEFAULT_TIME_SEND_MAIL;

            //defectos

            if (request.NumberDefects > 0)
                t19.Weight = DEFECT_WEIGHT;
            else
                t19.ReportTime = 0;

            //t3 CORREO VALIDAR DATOS PRODUCCION solo ciclo 1  5min
            if (request.Cycle == 1)
                t3.ReportTime = DEFAULT_TIME_SEND_MAIL;
            else
                t3.ReportTime = 0;

            //t8 VALIDAR INSTALACION CICLO PREVIO  solo para ciclos no 1  variable 
            if (request.Cycle > 1)
                t8.Weight = 2*WEIGHT_1;
            else
                t8.ReportTime = 0;


            //t6 DESCARGAR FUENTES VERSION PREVIA OP solo sga  variable
            if (request.NumberPBL > 0)
                t6.Weight = WEIGHT_1;
            else
                t6.ReportTime = 0;


            //t7 COMPARAR FUENTES  variable
            t7.Weight = 2*WEIGHT_1;





            //t13 INSTALACION DE COMPILADOS   variable
            //t12 VALIDAR AMBIENTE PRE INSTALACIÓN  varible
            //t15 VALIDAR AMBIENTE POST INSTALACIÓN   variable
            if (request.Installation)
            {
                t13.Weight =6*WEIGHT_1;
                t12.Weight = WEIGHT_1;
                t15.Weight = WEIGHT_1;
                if (request.DDS)
                {
                    //t14 CONFIGURAR DDS   variable
                    t14.Weight = 2.5m *WEIGHT_1;
                }
                else
                {
                    t14.ReportTime = 0;
                }


                //t9 COMPILAR FUENTES  seCompilo  variable
                //t10 SUBIR COMPILADOS RTC seSubioEjecutables variable
                //t11 DESCARGAR COMPILADOS RTC seSubioEjecutables  variable
                if (request.SourcesCompiled)
                {
                    if (request.NumberPBL > 0 )
                    {
                        t9.ReportTime = request.NumberPBL * TIME_PBL_COMPILAR;

                        if (t9.ReportTime > request.TimeSpent)
                        {
                            t9.ReportTime = 99999;
                            t9.Weight = 6 * WEIGHT_1;
                        }
                       
                    }
                    else
                    {
                        t9.Weight = 3 * WEIGHT_1;
                    }
                    t10.Weight = 2 * WEIGHT_1;
                    t11.Weight = 2 * WEIGHT_1;
                }
                else
                {
                    t9.ReportTime = 0;
                    t10.ReportTime = 0;
                    t11.ReportTime = 0;
                }

            }
            else
            {
                t13.ReportTime = 0;
                t12.ReportTime = 0;
                t15.ReportTime = 0;
                t14.ReportTime = 0;
                t9.ReportTime = 0;
                t10.ReportTime = 0;
                t11.ReportTime = 0;

            }

            //t17 ELABORAR EVIDENCIAS CM  variable 
            //t18 VALIDAR MDI CHECKLIST   variable
            t17.Weight = 1.5m * WEIGHT_1;
            t18.Weight = 1.5M * WEIGHT_1;


            //pases especiales
            if (request.Special)
            {
                t10.ReportTime = 0;
                t11.ReportTime = 0;
            }
           


            assignation.Tasks.Add(t1);
            assignation.Tasks.Add(t2);
            assignation.Tasks.Add(t3);
            assignation.Tasks.Add(t4);
            assignation.Tasks.Add(t5);
            assignation.Tasks.Add(t6);
            assignation.Tasks.Add(t7);
            assignation.Tasks.Add(t8);
            assignation.Tasks.Add(t9);
            assignation.Tasks.Add(t10);
            assignation.Tasks.Add(t11);
            assignation.Tasks.Add(t12);
            assignation.Tasks.Add(t13);
            assignation.Tasks.Add(t14);
            assignation.Tasks.Add(t15);
            assignation.Tasks.Add(t16);
            assignation.Tasks.Add(t17);
            assignation.Tasks.Add(t18);
            assignation.Tasks.Add(t19);
            assignation.Tasks.Add(t20);


            decimal allocatedTime = 0;
            decimal notAllocatedWeightTotal = 0;

            foreach (var task in assignation.Tasks)
            {
                if (task.ReportTime != 99999)
                {
                    allocatedTime += task.ReportTime;
                   
                }
                else
                {
                    notAllocatedWeightTotal += task.Weight;
                   
                }
            }
          
            var availableTime = request.TimeSpent- allocatedTime;
        

            foreach (var task in assignation.Tasks)
            {
                if (task.ReportTime == 99999)
                {
                    task.ReportTime = Math.Round(task.Weight / notAllocatedWeightTotal * (availableTime),2);
                

                }
            }
            decimal timeofalltasks = 0;
            foreach (var task in assignation.Tasks)
            {
                timeofalltasks += task.ReportTime;
            }

           
            if (timeofalltasks != request.TimeSpent)
            {

                if (t13.ReportTime>0)
                    t13.ReportTime -= (timeofalltasks - request.TimeSpent);
                else
                    t18.ReportTime -= (timeofalltasks - request.TimeSpent);
            }

            
     

           
            decimal totalReportTime = 0;
            foreach (var task in assignation.Tasks)
            {
                
                totalReportTime += task.ReportTime;

            }

          

            return assignation;
        }

    }
}
