		//Buen día Ingeniero espacial le presento mi modificación de la nave “Platinum flight”
		//Tienes un centro de mando del DLC “SparksOfTheFuture” los paneles de este centro de mando están totalmente funcionales que trabajan 
		//junto al bloque programable, que nos dan la perspectiva sobre el motor de salto, la munición de la torreta interior encima de la nave, 
		//el hidrogeno y oxígeno, la energía con el estado de baterías y estado del reactor, 
		//dentro de la nave disponemos un LCD transparente que nos da el inventario actual del contenedor grande de la nave. 
		
		//En la parte superior tenemos 2 LCD transparentes que nos dan un alcance general de la nave como carga máxima, peso, velocidad y los créditos mios.
		
		//Tenemos un DLC automático para poder si la habitación se encuentra presurizada o no.
		//El Script que está en el bloque programable esta totalmente abierto a edición, sin ninguna ofuscación, 
		//es 100% creado por mi “DrHousexx – DrCardenak” tienen la libertad de editarlo, algunas partes están en desorden, 
		//pero creo que se entiende lo que se deseo realizar.
		//Solo se pide que al reciclar o reutilizar el código se respeten los créditos :3 pero queda a su criterio
		//Disfruten de mi trabajo.
		//Créditos:
		//Atte: DrHousexx 
		//Steam Profile: https://steamcommunity.com/id/drhousexx/
		//GitHub: https://github.com/DrHousexx
		//WebSite Developer:  https://CaritaKawai.com/
 

        string Cabina = "Asiento de control.PLT";
        IMyCockpit cockpit;

        string JumpDrive = "MotordeSalto3.PLT";
        int PantallaMotor = 2;
        IMyJumpDrive MotorDeSalto;
        IMyTextSurface textMotor;

        string TanqueHidrogeno = "Hydrogen Tank.PLT";
        string TanqueOxigeno = "Oxygen Tank.PLT";
        int PantallaHidrogeno = 1;
        IMyGasTank TanqueH;
        IMyGasTank TanqueO;
        IMyTextSurface TanqueHTexto;

        string NTorreta = "Interior Turret.PLT";
        int PantallaTorreta = 3;
        IMyLargeInteriorTurret Torreta;
        IMyTextSurface TextoTorreta;

        string GrupoBaterias = "Baterias.PLT";
        int PantallaBaterias = 4;
        List<IMyTerminalBlock> bloques;
        IMyTextSurface TextoContenedor;

        //PantallaPrincipal
        IMyTextSurface TextoGenerales;
        int PantallaGenerales = 0;

        //Grupo de Propulsores
        IMyBlockGroup propulsores;
        string NPropulsores = "Hydrogen Thrusters";
        List<IMyTerminalBlock> bloques2;

        //LCD Grande Interior
        string NContenedor = "Large Cargo Container.PLT";
        string NReactor = "Small Reactor.PLT";
        string LCD = "LCD transparente.PLT";
        IMyTextSurface panel;
        IMyCargoContainer Contenedor;
        IMyReactor Reactor;

        //Ventilacion
        string Ventila = "Air Vent.PLT";
        string LCDVentila = "LCD de esquina superior 2.PLT";
        IMyAirVent vent;
        IMyTextSurface Prezurizacion;

        //Bloque Programable
        string NBloqueProgram = "Bloque programable.PLT";
        int NroPantallaBP = 0;
        IMyProgrammableBlock BloqueProgramable;
        IMyTextSurface PanelBloqueProgram;

        //Panel LCD3 Y LCD2
        string LCD2 = "LCD transparente 2.PLT";
        string LCD3 = "LCD transparente 3.PLT";
        IMyTextSurface Lcd2;
        IMyTextSurface Lcd3;

        string tab = "\n";
        int acumulador = 0;
        bool flagacumulador = true;
        int acumulador2 = 0;
        bool flagacumulador2 = true;
        public Program()
        {
            //Frecuencia de refresco
            Runtime.UpdateFrequency = UpdateFrequency.Update10;
            //Inicializamos los bloques
            MotorDeSalto = GridTerminalSystem.GetBlockWithName(JumpDrive) as IMyJumpDrive;
            cockpit = GridTerminalSystem.GetBlockWithName(Cabina) as IMyCockpit;
            TanqueH = GridTerminalSystem.GetBlockWithName(TanqueHidrogeno) as IMyGasTank;
            TanqueO = GridTerminalSystem.GetBlockWithName(TanqueOxigeno) as IMyGasTank;
            Torreta = GridTerminalSystem.GetBlockWithName(NTorreta) as IMyLargeInteriorTurret;
            Contenedor = GridTerminalSystem.GetBlockWithName(NContenedor) as IMyCargoContainer;
            Reactor = GridTerminalSystem.GetBlockWithName(NReactor) as IMyReactor;
            vent = GridTerminalSystem.GetBlockWithName(Ventila) as IMyAirVent;
            BloqueProgramable = GridTerminalSystem.GetBlockWithName(NBloqueProgram) as IMyProgrammableBlock;
            //MotorDeSaltoTexto
            textMotor = cockpit.GetSurface(PantallaMotor); // range de 0 al 5 depende del numero de paneles del cockpit
            textMotor.ContentType = VRage.Game.GUI.TextPanel.ContentType.TEXT_AND_IMAGE;
            textMotor.FontSize = 1.6f;
            textMotor.FontColor = Color.Aquamarine;
            //TanqueHidrogenoOxigenoTexto
            TanqueHTexto = cockpit.GetSurface(PantallaHidrogeno);
            TanqueHTexto.ContentType = VRage.Game.GUI.TextPanel.ContentType.TEXT_AND_IMAGE;
            TanqueHTexto.FontSize = 1.15f;
            TanqueHTexto.FontColor = Color.Aquamarine;
            //TorretaInterior
            TextoTorreta = cockpit.GetSurface(PantallaTorreta);
            TextoTorreta.ContentType = VRage.Game.GUI.TextPanel.ContentType.TEXT_AND_IMAGE;
            TextoTorreta.FontSize = 3.15f;//3.15
            TextoTorreta.FontColor = Color.Red;
            //Baterias
            TextoContenedor = cockpit.GetSurface(PantallaBaterias);
            TextoContenedor.ContentType = VRage.Game.GUI.TextPanel.ContentType.TEXT_AND_IMAGE;
            TextoContenedor.FontSize = 1.50f;
            TextoContenedor.FontColor = Color.Aquamarine;
            bloques = new List<IMyTerminalBlock>();
            IMyBlockGroup baterias = GridTerminalSystem.GetBlockGroupWithName(GrupoBaterias);
            baterias.GetBlocks(bloques);
            //Panel LCD de contenedor 
            panel = GridTerminalSystem.GetBlockWithName(LCD) as IMyTextSurface;
            panel.ContentType = VRage.Game.GUI.TextPanel.ContentType.TEXT_AND_IMAGE;
            panel.FontColor = Color.Aquamarine;
            panel.FontSize = 0.886f;
            panel.Font = "Monospace";
            //Panel texto General
            TextoGenerales = cockpit.GetSurface(PantallaGenerales);
            TextoGenerales.ContentType = VRage.Game.GUI.TextPanel.ContentType.TEXT_AND_IMAGE;
            TextoGenerales.FontColor = Color.Aquamarine;
            TextoGenerales.Font = "Monospace";
            TextoGenerales.FontSize = 0.8f;
            //Propulsores
            bloques2 = new List<IMyTerminalBlock>();
            propulsores = GridTerminalSystem.GetBlockGroupWithName(NPropulsores);
            propulsores.GetBlocks(bloques2);
            //Presurizacion
            Prezurizacion = GridTerminalSystem.GetBlockWithName(LCDVentila) as IMyTextSurface;
            Prezurizacion.ContentType = VRage.Game.GUI.TextPanel.ContentType.TEXT_AND_IMAGE;
            //Panel de BloqueProgramable
            PanelBloqueProgram = BloqueProgramable.GetSurface(NroPantallaBP);
            PanelBloqueProgram.ContentType = VRage.Game.GUI.TextPanel.ContentType.TEXT_AND_IMAGE;
            PanelBloqueProgram.FontColor = Color.White;
            //LCD2 Y LCD3
            Lcd2 = GridTerminalSystem.GetBlockWithName(LCD2) as IMyTextSurface;
            Lcd2.ContentType = VRage.Game.GUI.TextPanel.ContentType.TEXT_AND_IMAGE;
            Lcd2.FontColor = Color.White;
            Lcd2.FontSize = 0.886f;
            Lcd2.Font = "Monospace";
            Lcd3 = GridTerminalSystem.GetBlockWithName(LCD3) as IMyTextSurface;
            Lcd3.ContentType = VRage.Game.GUI.TextPanel.ContentType.TEXT_AND_IMAGE;
            Lcd3.FontColor = Color.White;
            Lcd3.FontSize = 0.886f;
            Lcd3.Font = "Monospace";
        }
        public void Main(string argument, UpdateType updateSource)
        {
            //Motor de Salto
            string texto0 = "";
            float Distancia = 0;
            Distancia = MotorDeSalto.JumpDistanceMeters;
            Distancia /= 1000;
            texto0 += "Motor de Saltos(JumpDrive)" + tab;
            texto0 += "Distancia en Km:" + tab + Distancia.ToString() + tab;
            texto0 += "Carga Maxima: " + ((MotorDeSalto.MaxStoredPower)).ToString() + tab;
            texto0 += "Carga Actual: " + ((MotorDeSalto.CurrentStoredPower)).ToString() + tab;
            texto0 += "Distancia Maxima: " + ((MotorDeSalto.MaxJumpDistanceMeters) / 1000).ToString() + tab;
            texto0 += "Distancia Minima: " + ((MotorDeSalto.MinJumpDistanceMeters) / 1000).ToString() + tab;
            textMotor.WriteText(texto0);
            //Tanque de Hidrogeno
            double aux = 0;
            string aux2 = "";
            string textoH = "";
            textoH += "Tanque de Hidrógeno" + tab;
            aux = ((TanqueH.FilledRatio) * TanqueH.Capacity);
            aux2 = String.Format("{0:0.0000}", aux);
            textoH += "Capacidad Actual: " + aux2 + "L" + tab;
            textoH += "Capacidad Maxima: " + (("15.000.000")).ToString() + tab;
            textoH += "Porcentaje de llenado: " + Convert.ToInt32((TanqueH.FilledRatio * 100)).ToString() + "%" + tab;
            string barraH1 = "[";
            for (int i = 0; i < 72; i++)
            {
                double llenado = ((TanqueH.FilledRatio * 100) * 72) / 100;
                if (llenado <= i)
                    barraH1 += "'";
                else
                    barraH1 += "|";
            }
            barraH1 += "]";
            textoH += barraH1 + tab;
            //Tanque de Hoxigeno
            textoH += "Tanque de Oxigeno" + tab;
            aux = ((TanqueO.FilledRatio) * TanqueO.Capacity);
            aux2 = String.Format("{0:0.0000}", aux);
            textoH += "Capacidad Actual: " + aux2 + "L" + tab;
            textoH += "Capacidad Maxima: " + (("100.000")).ToString() + tab;
            textoH += "Porcentaje de llenado: " + Convert.ToInt32((TanqueO.FilledRatio * 100)).ToString() + "%" + tab;
            barraH1 = "[";
            for (int i = 0; i < 72; i++)
            {
                double llenado = ((TanqueO.FilledRatio * 100) * 72) / 100;
                if (llenado <= i)
                    barraH1 += "'";
                else
                    barraH1 += "|";
            }
            barraH1 += "]";
            textoH += barraH1 + tab;
            TanqueHTexto.WriteText(textoH);//Escritura de los tanques
            //Torreta Interior
            string textoT = "";
            textoT += " |AMMO|" + tab;

            List<MyInventoryItem> items = new List<MyInventoryItem>();
            Torreta.GetInventory(0).GetItems(items);
            if (items.Count == 0)
                textoT += "NO AMMO";
            else
            {
                textoT += "--" + items[0].Amount + "--" + tab;
                textoT += "Charges" + tab + "25x184mm" + tab;
                if (Torreta.IsShooting == true)
                    textoT += "!!!!" + tab;
            }
            textoT += items[0].Type.SubtypeId;
            TextoTorreta.WriteText(textoT);
            //Para las Baterias
            string textoBA = "";
            float EnergiaEntrante = 0;
            float EnergiaMaxima = 0;
            float EnergiaActual = 0;
            int nrobaterias = 0;
            foreach (var bateria in bloques)
            {
                var batery = bateria as IMyBatteryBlock;
                EnergiaEntrante += batery.CurrentInput;
                EnergiaMaxima += batery.MaxStoredPower;
                EnergiaActual += batery.CurrentStoredPower;
                nrobaterias++;
            }
            textoBA += "----Energia batteries: " + Convert.ToInt32((100 * EnergiaActual) / EnergiaMaxima).ToString() + "%----" + tab;
            textoBA += "[";
            for (int i = 0; i < 57; i++)
            {
                double llenado = (((100 * EnergiaActual) / EnergiaMaxima) * 57) / 100;
                if (llenado <= i)
                    textoBA += "'";
                else
                    textoBA += "|";
            }
            textoBA += "]" + tab;
            textoBA += "Energia Actual: " + String.Format("{0:0.00}", EnergiaActual) + " MWh" + tab;
            textoBA += "Energia Maxima: " + String.Format("{0:0.00}", EnergiaMaxima) + " MWh" + tab;
            //textoBA += "Total de Baterias: " + nrobaterias; 
            //Para el Reactor
            textoBA += "---------Estado Reactor---------" + tab;
            textoBA += "Salida Actual: " + Reactor.CurrentOutput + "MW" + tab;
            textoBA += "Salida Maxima: " + Reactor.MaxOutput + tab;
            List<MyInventoryItem> itemsR = new List<MyInventoryItem>();
            Reactor.GetInventory(0).GetItems(itemsR);
            if (itemsR.Count == 0)
            {
                textoBA += "URANIUM EMPTY!!!" + tab;
                TextoContenedor.FontColor = Color.Red;
            }
            else
            {
                TextoContenedor.FontColor = Color.Aquamarine;
                textoBA += "--" + itemsR[0].Amount + "<--->" + itemsR[0].Type.SubtypeId + tab;
            }
            TextoContenedor.WriteText(textoBA);
            //Contenedor Grande LCD X16 X18+7
            string textoC = "";
            List<MyInventoryItem> itemsc = new List<MyInventoryItem>();
            Contenedor.GetInventory(0).GetItems(itemsc);
            if (itemsc.Count == 0)
                textoC = "INVENTARIO VACIO - EMPTY";
            else
            {
                textoC = "[" + Contenedor.CustomName + "]" + tab;
                if (itemsc.Count > 18)
                {
                    if (flagacumulador)
                    {
                        flagacumulador = (acumulador == 20 ? false : true);
                        acumulador++;
                        for (int x = 0; x < 18; x++)
                        {
                            textoC += "[" + itemsc[x].Type.SubtypeId + "]";
                            for (int i = (itemsc[x].Type.SubtypeId).Length; i < 20; i++)
                            {
                                textoC += ".";
                            }
                            textoC += "" + itemsc[x].Amount + "" + tab;
                        }
                    }
                    else
                    {
                        flagacumulador = (acumulador == 0 ? true : false);
                        acumulador--;
                        for (int x = 18; x < itemsc.Count; x++)
                        {
                            textoC += "[" + itemsc[x].Type.SubtypeId + "]";
                            for (int i = (itemsc[x].Type.SubtypeId).Length; i < 20; i++)
                            {
                                textoC += ".";
                            }
                            textoC += "" + itemsc[x].Amount + "" + tab;
                        }
                    }
                }
                else
                {
                    for (int x = 0; x < itemsc.Count; x++)
                    {
                        textoC += "[" + itemsc[x].Type.SubtypeId + "]";
                        for (int i = (itemsc[x].Type.SubtypeId).Length; i < 20; i++)
                        {
                            textoC += ".";
                        }
                        textoC += "" + itemsc[x].Amount + "" + tab;
                    }
                }
            }
            panel.WriteText(textoC);
            //Generales - Propulsores y Coordenadas MEJOR NO TOCAR NO ESTA AUTOMATIZADO
            string textG = "";

            string pA = "";//alt+176 
            string pB = "";
            string pC = "";
            string pD = "";
            string pE = "";
            string pF = "";
            string pG = "";

            int auxc = 1;
            foreach (var propulsor in bloques2)
            {
                var thruster = propulsor as IMyThrust;
                switch (auxc)
                {
                    case 1:
                        pA = thruster.CurrentThrust > 0 ? "○" : "◙";
                        auxc++;
                        break;
                    case 2:
                        pB = thruster.CurrentThrust > 0 ? "○" : "◙";
                        auxc++;
                        break;
                    case 3:
                        pC = thruster.CurrentThrust > 0 ? "○" : "◙";
                        auxc++;
                        break;
                    case 4:
                        pD = thruster.CurrentThrust > 0 ? "○" : "◙";
                        auxc++;
                        break;
                    case 5:
                        pE = thruster.CurrentThrust > 0 ? "○" : "◙";
                        auxc++;
                        break;
                    case 6:
                        pF = thruster.CurrentThrust > 0 ? "○" : "◙";
                        auxc++;
                        break;
                    case 7:
                        pG = thruster.CurrentThrust > 0 ? "○" : "◙";
                        auxc++;
                        break;
                    default:
                        break;
                }
            }
            string espacio = "----------";
            textG += "Diagrama de los Propulsores |H|" + tab;
            textG += "Leyenda ◙ = OFF , ○ = ON " + tab + tab;

            textG += espacio + "--------------------" + tab;
            textG += espacio + pF + "--------" + pD + "----------" + tab;
            textG += espacio + "------" + pA + "--" + pB + "-" + pG + "--------" + tab;
            textG += espacio + pE + "--------" + pC + "----------" + tab;
            textG += espacio + "--------------------" + tab + tab;

            textG += "Coordenadas Actuales: " + tab;
            textG += "     X:" + cockpit.CenterOfMass.X.ToString() + tab;
            textG += "     Y:" + cockpit.CenterOfMass.Y.ToString() + tab;
            textG += "     Z:" + cockpit.CenterOfMass.Z.ToString() + tab;
            TextoGenerales.WriteText(textG);

            //Presurizacion
            string textPre = "";
            Prezurizacion.FontSize = 6.5f;
            if(vent.Status.ToString() == "Depressurizing")
            {
                Prezurizacion.FontColor = Color.Red;
                textPre += "Habitacion Despresurizada";
            }
            else if(vent.Status.ToString() == "Pressurized")
            {
                Prezurizacion.FontColor = Color.Aquamarine;
                textPre += " Habitacion Presurizada";
            }
            else
            {
                Prezurizacion.FontColor = Color.Aquamarine;
                textPre += " Habitacion Presurizada";
            }
            Prezurizacion.WriteText(textPre); //Depressurizing - Pressurized
            //Bloque Programable
            string textBloqueP = "";
            textBloqueP += BloqueProgramable.IsRunning.ToString() + tab;
            textBloqueP += BloqueProgramable.IsFunctional ? "100% Operativo" + tab : "ERROR" + tab;
            if (flagacumulador2)
            {
                flagacumulador2 = (acumulador2 == 5 ? false : true);
                acumulador2++;
                textBloqueP += "------------------------";
            }
            else
            {
                flagacumulador2 = (acumulador2 == 0 ? true : false);
                acumulador2--;
                textBloqueP += "||||||||||||||||||||||||||||||||";
            }
            PanelBloqueProgram.WriteText(textBloqueP + tab + tab + "A [DrHousexx] Script");
            //Panel LCD Transparente 3 - LCD transparente 3.PLT
            Lcd3.FontSize = 0.82f;
            string lcd3t = "";
            double MaxVolume = Double.Parse((Contenedor.GetInventory(0).MaxVolume * 1000).ToString());
            double CurrentVolume = Double.Parse((Contenedor.GetInventory(0).CurrentVolume * 1000).ToString());
            double CurrentMass = Double.Parse((Contenedor.GetInventory(0).CurrentMass).ToString());

            lcd3t += "----Cargo Maximo: " + Convert.ToInt32((100 * CurrentVolume) / MaxVolume).ToString() + "% usado----" + tab + Contenedor.CustomName + tab;
            lcd3t += "|";
            for (int i = 0; i < 29; i++)
            {
                double llenado = (((100 * CurrentVolume) / MaxVolume) * 20) / 100;
                if (llenado <= i)
                    lcd3t += "-";
                else
                    lcd3t += "█";
            }
            lcd3t += "|" + tab;
            lcd3t += "Volumen Maximo: " + MaxVolume +"L m^3"+ tab;
            lcd3t += "Volumen Ocupado: " + CurrentVolume +"L m^3"+ tab;
            lcd3t += tab + "Masa actual (Peso): " + tab + CurrentMass + "Kg" + tab + tab+tab;

            lcd3t += "Generales: " + tab+tab +"Masa total de la Nave: " +tab +cockpit.CalculateShipMass().TotalMass.ToString() + "Kg" + tab;
            lcd3t += "Velocidad Actual de la nave: " +tab+ cockpit.GetShipSpeed​​().ToString() + "m/s"+tab;
            lcd3t += (cockpit.GetShipSpeed​​() * 3.6).ToString() + "Km/h";

            Lcd3.WriteText(lcd3t);
            //Panel LCD Transparente 2 - LCD transparente 2.PLT
            Lcd2.WriteText(textoBA + tab + textoH);

        }