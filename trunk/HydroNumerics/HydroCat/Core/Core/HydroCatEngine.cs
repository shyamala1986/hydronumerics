﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using HydroNumerics.Time.Core;

namespace HydroNumerics.HydroCat.Core
{
    public class HydroCatEngine
    {


        #region ===== Time series ======
        public BaseTimeSeries PrecipitationTs { get; set; }  //Input
        public BaseTimeSeries PotentialEvaporationTs { get; set; }  //Input
        public BaseTimeSeries TemperatureTs { get; set; } // Input
        public BaseTimeSeries ObservedRunoffTs { get; set; } // input

        public TimeSeriesGroup OutputTsg { get; private set; }

        private TimestampSeries runoffTs;

        public TimespanSeries RunoffTs { get; private set; }

        #endregion

        //public InitialValues InitialValues { get; private set; }

        //public Parameters Parameters { get; private set; }

        #region  ====== Initial values ======================================
        /// <summary>
        /// Snow storage [Unit: millimiters] (Ss)
        /// </summary>
        [CategoryAttribute("Initial values")]
        public double InitialSnowStorage { get; set; }

        /// <summary>
        /// Surface Storage [Unit: millimiters] (U)
        /// </summary>
        [CategoryAttribute("Initial values")]
        public double InitialSurfaceStorage { get; set; }

        /// <summary>
        /// Root zone storage [Unit: millimiters] (L)
        /// </summary>
        [CategoryAttribute("Initial values")]
        public double InitialRootZoneStorage { get; set; }

        /// <summary>
        /// Overland flow rate (specific flow, before routing) [Unit: Millimiters / day]
        /// </summary>
        [CategoryAttribute("Initial values")]
        public double InitialOverlandFlow { get; set; }

        /// <summary>
        /// Inter flow rate (specific flow, before routing) [Unit: millimiters / day]
        /// </summary>
        [CategoryAttribute("Initial values")]
        public double InitialInterFlow { get; set; }

        /// <summary>
        /// Base flow rate (specific flow, before routing) [Unit: millimiters / day]
        /// </summary>
        [CategoryAttribute("Initial values")]
        public double InitialBaseFlow { get; set; }
        #endregion

        #region =========  Calibration parameters =====================
        /// <summary>
        /// Catchment area [unit: m2] (Area)
        /// </summary>
        [CategoryAttribute("Calibration parameter")]
        public double CatchmentArea { get; set; }

        /// <summary>
        /// Surface water storage capacity (max capacity) [Unit: millimiters] (U*)
        /// </summary>
        [CategoryAttribute("Calibration parameter")]
        public double SurfaceStorageCapacity { get; set; }

        /// <summary>
        /// Root zone capacity [Unit: millimiters] (L*)
        /// </summary>
        [CategoryAttribute("Calibration parameter")]
        public double RootZoneStorageCapacity { get; set; }

        /// <summary>
        /// Snow melting coefficient [Unit: dimensionless] (Cs)
        /// </summary>
        [CategoryAttribute("Calibration parameter")]
        public double SnowmeltCoefficient { get; set; }

        /// <summary>
        /// Overland flow coefficient [Unit: dimensionless] (Cof)
        /// Determins the fraction of excess water that runs off as overland flow
        /// </summary>
        [CategoryAttribute("Calibration parameter")]
        public double OverlandFlowCoefficient { get; set; }

        /// <summary>
        /// Overland flow routing time constant [Unit: Days]  (Ko)
        /// </summary>
        [CategoryAttribute("Calibration parameter")]
        public double OverlandFlowTimeConstant { get; set; }

        /// <summary>
        /// Overland flow treshold [Unit: dimensionless] (TOF) (CL2)
        /// If the relative moisture content of the roor zone is above the overland flow treshold
        /// overland flow is generated. The overland flow treshold must be in the interval [0,1].
        /// </summary>
        [CategoryAttribute("Calibration parameter")]
        public double OverlandFlowTreshold { get; set; }

        /// <summary>
        /// Interflow coefficient. [Unit: dimensionless] (CIf)
        /// Must be in the interval [0,1]
        /// </summary>
        [CategoryAttribute("Calibration parameter")]
        public double InterflowCoefficient { get; set; }

        /// <summary>
        /// Interflow routing time constant [unit: Days]
        /// </summary>
        [CategoryAttribute("Calibration parameter")]
        public double InterflowTimeConstant { get; set; }

        /// <summary>
        /// Interflow treshold [Unit: millimiters] (CL1)
        /// Must be in the interval [0,1]
        /// </summary>
        [CategoryAttribute("Calibration parameter")]
        public double InterflowTreshold { get; set; }

        /// <summary>
        /// Base flow routing time constant[Unit: Days] (CKBF) (kb)
        /// </summary>
        [CategoryAttribute("Calibration parameter")]
        public double BaseflowTimeConstant { get; set; }

        #endregion --- Calibration parameters ------

        #region ======   Simulation control input parameters ================
        /// <summary>
        /// Start time for the simulation
        /// </summary>
        public DateTime SimulationStartTime { get; set; }

        /// <summary>
        /// End time for the simulation
        /// </summary>
        public DateTime SimulationEndTime { get; set; }
        #endregion

        // ============= output ================================

         /// <summary>
         /// The calculated specific runoff [Unit: mm/day]
         /// </summary>
        public double SpecificRunoff { get; private set; }
        
        /// <summary>
        /// The calculated riverflow [Unit: m3/sec]
        /// </summary>
        public double Runoff { get; private set; }


        #region ==== state variables =====
        /// <summary>
        /// Snow storage [Unit: millimiters] (Ss)
        /// </summary>
        [CategoryAttribute("State variables")]
        public double SnowStorage { get; private set; }

        /// <summary>
        /// Surface Storage [Unit: millimiters] (U)
        /// </summary>
        [CategoryAttribute("State variables")]
        public double SurfaceStorage { get; private set; }

        /// <summary>
        /// Root zone storage [Unit: millimiters] (L)
        /// </summary>
        [CategoryAttribute("State variables")]
        public double RootZoneStorage { get; private set; }

        /// <summary>
        /// Overland flow rate (specific flow, before routing) [Unit: Millimiters / day]
        /// </summary>
        [CategoryAttribute("State variables")]
        public double OverlandFlow { get; private set; }

        /// <summary>
        /// Inter flow rate (specific flow, before routing) [Unit: millimiters / day]
        /// </summary>
        [CategoryAttribute("State variables")]
        public double InterFlow { get; private set; }

        /// <summary>
        /// Base flow rate (specific flow, before routing) [Unit: millimiters / day]
        /// </summary>
        [CategoryAttribute("State variables")]
        public double BaseFlow { get; private set; }
        #endregion

        // ----------
        HydroNumerics.Core.Unit mmPrDayUnit; //
        HydroNumerics.Core.Unit centigradeUnit;
        HydroNumerics.Core.Unit m3PrSecUnit;

        int timestep = 0;
        double[] precipitation; //time sereis are copied to these arrays for performance reasons.
        double[] potentialEvaporation;
        double[] temperature;
 
        public bool IsInitialized { get; private set; }
       
        //public DateTime CurrentTime { get; private set; }
        int numberOfTimesteps;

        public HydroCatEngine()
        {
            IsInitialized = false;
            
            //--- Default values ----
            SimulationStartTime = new DateTime(2010, 1, 1);
            SimulationEndTime = new DateTime(2011, 1, 1);
            //CurrentTime = SimulationStartTime.AddDays(0);

            //-- Default values (state variables)
            SnowStorage = InitialSnowStorage = 0;
            SurfaceStorage = InitialSurfaceStorage = 0;
            RootZoneStorage = InitialRootZoneStorage = 220;
            OverlandFlow = InitialOverlandFlow = 0;
            InterFlow = InitialInterFlow = 0;
            BaseFlow = InitialBaseFlow = 0.6;

            //-- Default values (parameters)
            this.CatchmentArea = 160000000;
            this.SnowmeltCoefficient = 2.0;
            this.SurfaceStorageCapacity = 18;
            this.RootZoneStorageCapacity = 250;
            this.OverlandFlowCoefficient = 0.61;
            this.InterflowCoefficient = 0.6; //??
            this.OverlandFlowTreshold = 0.38;
            this.InterflowTreshold = 0.08;
            this.OverlandFlowTimeConstant = 0.3;
            this.InterflowTimeConstant = 30;
            this.BaseflowTimeConstant = 2800;

            // -- Units --
            mmPrDayUnit = new HydroNumerics.Core.Unit("mm pr day", 1.0/(1000*3600*24), 0);
            centigradeUnit = new HydroNumerics.Core.Unit("Centigrade", 1.0, -273.15);
            m3PrSecUnit = new HydroNumerics.Core.Unit("m3 pr sec.", 1.0, 0.0);

            
            // --- 
            //InitialValues = new InitialValues();
            //Parameters = new Parameters();

 
        }

        public void Initialize()
        {
            // -- reset initial values ---
            SnowStorage = InitialSnowStorage;
            SurfaceStorage = InitialSurfaceStorage;
            RootZoneStorage = InitialRootZoneStorage;
            OverlandFlow = InitialOverlandFlow;
            InterFlow = InitialInterFlow;
            BaseFlow = InitialBaseFlow;

            // -- prepare input arrays ---
            numberOfTimesteps = (int)(SimulationEndTime.ToOADate() - SimulationStartTime.ToOADate() - 0.5);
            precipitation = new double[numberOfTimesteps];
            potentialEvaporation = new double[numberOfTimesteps];
            temperature = new double[numberOfTimesteps];
            for (int i = 0; i < numberOfTimesteps; i++)
            {
                DateTime fromTime = SimulationStartTime.AddDays(i);
                DateTime toTime = SimulationStartTime.AddDays(i + 1);
                precipitation[i] = PrecipitationTs.GetValue(fromTime, toTime, mmPrDayUnit);
                potentialEvaporation[i] = PotentialEvaporationTs.GetValue(fromTime, toTime, mmPrDayUnit);
                temperature[i] = TemperatureTs.GetValue(fromTime, toTime, centigradeUnit);
            }

            //CurrentTime = SimulationStartTime.AddDays(0);  //TODO : current time kan vist godt fjernes.

            // -- prepare output ---
            runoffTs = new TimestampSeries("Runoff", SimulationStartTime, numberOfTimesteps, 1, TimestepUnit.Days, 0);
            runoffTs.Unit = m3PrSecUnit;
            OutputTsg = new TimeSeriesGroup();
            OutputTsg.Items.Add(runoffTs);
            OutputTsg.Items.Add(ObservedRunoffTs);
            timestep = 0;
                                    
            IsInitialized = true;
        }

        public void RunSimulation()
        {
            Initialize();
            ValidateParametersAndInitialValues();

            for (int i = 0; i < numberOfTimesteps; i++)
            {
                PerformTimeStep();
            }
             
            
        }

        public void PerformTimeStep()
        {
 
            Step(precipitation[timestep], potentialEvaporation[timestep], temperature[timestep]);
            runoffTs.Items[timestep].Value = Runoff;
            
            timestep++;
        }
        
        public void Step(double precipitation, double potentialEvaporation, double temperature)
        {
            // extract values for current time step from the timeseries
           

            double yesterdaysOverlandFlow = OverlandFlow;
            double yesterdaysInterFlow = InterFlow;
            double yesterdaysBaseflow = BaseFlow;

            
            // 1) -- Precipitation, snowstorage, snow melt --
            if (temperature < 0)  
            {
                SnowStorage += precipitation;
            }
            else
            {   
                double snowMelt = Math.Min(SnowStorage, temperature * SnowmeltCoefficient);
                SnowStorage -= snowMelt;
                SurfaceStorage += (precipitation + snowMelt);
            }

            // 2) -- Surface evaporation --
            double surfaceEvaporation = Math.Min(SurfaceStorage, potentialEvaporation);
            SurfaceStorage -= surfaceEvaporation;


            // 3) -- Evaporation (evapotranspiration) from root zone
            if (surfaceEvaporation < potentialEvaporation)
            {
                double rootZoneEvaporation = (potentialEvaporation - surfaceEvaporation) * (RootZoneStorage / RootZoneStorageCapacity);
                RootZoneStorage -= rootZoneEvaporation;
            }


            // 4) --- Interflow ---
            if ((RootZoneStorage / RootZoneStorageCapacity) > InterflowTreshold)
            {
                InterFlow = InterflowCoefficient * Math.Min(SurfaceStorage, SurfaceStorageCapacity) * ((RootZoneStorage / RootZoneStorageCapacity) - InterflowTreshold) / (1 - InterflowTreshold);
            }
            SurfaceStorage -= InterFlow;

            // 5) Calculating Pn (Excess rainfall)
            double excessRainfall; //(Pn)
            if (SurfaceStorage > SurfaceStorageCapacity)
            {
                excessRainfall = SurfaceStorageCapacity - SurfaceStorage;
            }
            else
            {
                excessRainfall = 0;
            }

            SurfaceStorage -= excessRainfall;

            // 6) Overland flow calculation
            if ((RootZoneStorage / RootZoneStorageCapacity) > OverlandFlowTreshold)
            {
                OverlandFlow = OverlandFlowCoefficient * excessRainfall * ((RootZoneStorage / RootZoneStorageCapacity) - OverlandFlowTreshold) / (1 - OverlandFlowTreshold);
            }
            else
            {
                OverlandFlow = 0;
            }

            // 7) infiltration into the root zone (DL)
            double dl = (excessRainfall - OverlandFlow) / (1 - RootZoneStorage / RootZoneStorageCapacity);
            RootZoneStorage += dl;

            // 8) infiltration into the ground water zone
            double groundwaterInfiltration = excessRainfall - OverlandFlow - dl;
          
            // 9) Routing
            OverlandFlow = yesterdaysOverlandFlow * Math.Exp(-1 / OverlandFlowTimeConstant) + OverlandFlow * (1 - Math.Exp(-1 / OverlandFlowTimeConstant));

            InterFlow = yesterdaysInterFlow * Math.Exp(-1 /InterflowTimeConstant ) + InterFlow * (1 - Math.Exp(-1 / InterflowTimeConstant));

            BaseFlow = yesterdaysBaseflow * Math.Exp(-1 / BaseflowTimeConstant) + BaseFlow * (1 - Math.Exp(-1 / BaseflowTimeConstant));

            // 10) Runoff
            SpecificRunoff = OverlandFlow + InterFlow + BaseFlow;
            Runoff = CatchmentArea * SpecificRunoff / (1000.0 * 24 * 3600) ;
        }

        public void ValidateParametersAndInitialValues()
        {
            //-- State variable validation --
            GreaterThanOrEqualToZeroValidation(SnowStorage, "SnowStorage");
            GreaterThanOrEqualToZeroValidation(SurfaceStorage, "SurfaceStorage");
            GreaterThanOrEqualToZeroValidation(RootZoneStorage, "RootZoneStorage");
            GreaterThanOrEqualToZeroValidation(OverlandFlow, "OverlandFlow");
            GreaterThanOrEqualToZeroValidation(InterFlow, "InterFlow");
            GreaterThanOrEqualToZeroValidation(BaseFlow, "BaseFlow");

            //-- Parameter validation --
            GreaterThanOrEqualToZeroValidation(CatchmentArea, "CatchmentArea");

            GreaterThanOrEqualToZeroValidation(SurfaceStorageCapacity, "SurfaceStorageCapacity");

            GreaterThanOrEqualToZeroValidation(RootZoneStorageCapacity, "RootZoneStorageCapacity");

            GreaterThanOrEqualToZeroValidation(SnowmeltCoefficient, "SnowmeltCoefficient");

            GreaterThanOrEqualToZeroValidation(OverlandFlowTreshold, "OverlandFlowTreshold");
            UpperLimitValidation(OverlandFlowTreshold, "OverlandFlowTreshold", 1.0);

            GreaterThanOrEqualToZeroValidation(OverlandFlowCoefficient, "OverlandFlowCoefficient");
            UpperLimitValidation(OverlandFlowCoefficient, "OverlandFlowCoefficient", 1.0);

            GreaterThanOrEqualToZeroValidation(OverlandFlowTimeConstant, "OverlandFlowTimeConstant");

            GreaterThanOrEqualToZeroValidation(InterflowCoefficient, "InterflowCoefficient");
            UpperLimitValidation(InterflowCoefficient, "InterflowCoefficient", 1.0);

            GreaterThanOrEqualToZeroValidation(InterflowTreshold, "InterflowTreshold");
            UpperLimitValidation(InterflowTreshold, "InterflowTreshold", 1.0);

            GreaterThanOrEqualToZeroValidation(InterflowTimeConstant, "InterflowTimeConstant");

            GreaterThanOrEqualToZeroValidation(BaseflowTimeConstant, "BaseflowTimeConstant");
      
            
        }

        private void GreaterThanOrEqualToZeroValidation(double x, string variableName)
        {
            if (x < 0)
            {
                throw new Exception("The property <" + variableName + "> must be greather than zero. " + variableName + " = " + x.ToString());
            }
        }

        private void UpperLimitValidation(double x, string variableName, double upperLimit)
        {
            if (x > upperLimit)
            {
                throw new Exception("The property <" + variableName + "> must be less than or equal to " + upperLimit.ToString() + " " + variableName + " = " + x.ToString());
            }
        }

      


       

    }
}
