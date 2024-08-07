﻿using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace AutomobiliuNuoma.Core.Models
{
    public class NuomosUzsakymas
    {
        public Klientas Uzsakovas { get; set; }
        public Automobilis NuomuojamasAuto { get; set; }
        public DateTime NuomosPradzia { get; set; }
        public int DienuKiekis { get; set; }

        public string KlientoVardas {  get; set; }
        public string KlientoPavarde {  get; set; }
        public int AutomobilisId { get; set; }

        public int ElektromobilisId { get; set; }

        public int BenzAutomobilisId { get; set; }
        public int KlientasId { get; set; }

        public string AutoTipas { get; set; }

        [BsonId]
        public int UzsakymasId { get; set; }

        public int DarbuotojasId { get; set; }

        //public NuomosUzsakymas(int klientasId) { }

        //public NuomosUzsakymas(Klientas uzsakovas, Automobilis nuomuojamasAuto, DateTime nuomosPradzia, int dienuKiekis, Darbuotojas darbuotojasId)
        //{
        //    Uzsakovas = uzsakovas;
        //    NuomuojamasAuto = nuomuojamasAuto;
        //    NuomosPradzia = nuomosPradzia;
        //    DienuKiekis = dienuKiekis;
        //    DarbuotojasId = darbuotojasId;
        //}

        public NuomosUzsakymas(int klientasId, int automobilisId, DateTime nuomosPradzia, int dienuKiekis, string autoTipas, int darbuotojasId)
        {
            KlientasId = klientasId;
            AutomobilisId = automobilisId;
            NuomosPradzia = nuomosPradzia;
            DienuKiekis = dienuKiekis;
            AutoTipas = autoTipas;
            DarbuotojasId = darbuotojasId;
        }

        public NuomosUzsakymas()
        {
        }

        public decimal skaiciuotiNuomosKaina() /*REIKS PAKOREGUOTI IR PRIDETI PRIE CASE 8*/
        {
            return DienuKiekis * NuomuojamasAuto.NuomosKaina;
        }

        public DateTime gautiPabaigosData()
        {
            return NuomosPradzia.AddDays(DienuKiekis);
        }


    }


}
