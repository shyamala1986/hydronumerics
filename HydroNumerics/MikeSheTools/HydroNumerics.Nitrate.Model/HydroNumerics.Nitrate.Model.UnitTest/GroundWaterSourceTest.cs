﻿using HydroNumerics.Nitrate.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;

namespace HydroNumerics.Nitrate.Model.UnitTest
{
    
    
    /// <summary>
    ///This is a test class for GroundWaterSourceTest and is intended
    ///to contain all GroundWaterSourceTest Unit Tests
    ///</summary>
  [TestClass()]
  public class GroundWaterSourceTest
  {


    private TestContext testContextInstance;

    /// <summary>
    ///Gets or sets the test context which provides
    ///information about and functionality for the current test run.
    ///</summary>
    public TestContext TestContext
    {
      get
      {
        return testContextInstance;
      }
      set
      {
        testContextInstance = value;
      }
    }

    #region Additional test attributes
    // 
    //You can use the following additional attributes as you write your tests:
    //
    //Use ClassInitialize to run code before running the first test in the class
    //[ClassInitialize()]
    //public static void MyClassInitialize(TestContext testContext)
    //{
    //}
    //
    //Use ClassCleanup to run code after all tests in a class have run
    //[ClassCleanup()]
    //public static void MyClassCleanup()
    //{
    //}
    //
    //Use TestInitialize to run code before running each test
    //[TestInitialize()]
    //public void MyTestInitialize()
    //{
    //}
    //
    //Use TestCleanup to run code after each test has run
    //[TestCleanup()]
    //public void MyTestCleanup()
    //{
    //}
    //
    #endregion


    /// <summary>
    ///A test for GroundWaterSource Constructor
    ///</summary>
    [TestMethod()]
    public void LoadParticlesTest()
    {
      GroundWaterSource target = new GroundWaterSource();
      System.Diagnostics.Stopwatch stw = new Stopwatch();


      stw.Start();
      target.LoadParticles(@"E:\dhi\data\dkm\dk1\result\DK1_2014_pt_produktion.she - Result Files\PTReg_Extraction_1_Sink_Unsaturated_zone.shp");
      stw.Stop();
      stw.Reset();
      int k = 0;

      using (HydroNumerics.Geometry.Shapes.ShapeWriter sw = new Geometry.Shapes.ShapeWriter(@"d:\temp\unsatendpoints.shp"))
      {
        foreach (var m11 in target.Particles)
        {
          sw.WritePointShape(m11.X, m11.Y);
        }
      }


    }

    [TestMethod]
    public void LoadDaisyTest()
    {
      GroundWaterSource target = new GroundWaterSource();
      Stopwatch sw = new Stopwatch();
      sw.Start();

      target.LoadDaisyData(@"D:\DK_information\TestData\FileStructure\DaisyLeaching\SoilFarms_dmi10kmgrid_daily2007.txt");
      target.LoadDaisyData(@"D:\DK_information\TestData\FileStructure\DaisyLeaching\SoilFarms_dmi10kmgrid_daily2008.txt");
      target.LoadDaisyData(@"D:\DK_information\TestData\FileStructure\DaisyLeaching\SoilFarms_dmi10kmgrid_daily2009.txt");
      sw.Stop();



//      Assert.AreEqual(0.3305, target.leachdata.Grids[16510].TimeData.GetValues(new DateTime(2008, 4, 1), new DateTime(2009, 4, 1)).First(),0.0001);


      int k = 0;


    }
    private object Lock = new object();


    [TestMethod]
    public void CreateLeachFile()
    {
      MainViewModel mv = new MainViewModel();
      mv.LoadCatchments(@"D:\DK_information\TestData\FileStructure\id15_NSTmodel.shp");


      var leachdata = new DistributedLeaching();
      leachdata.LoadSoilCodesGrid(@"D:\DK_information\TestData\FileStructure\DaisyLeaching\DKDomainNodes_LU_Soil_codes.shp");

      var dist = leachdata.DaisyCodes.GetGridIdsWithInCatchment(mv.AllCatchments.Values);
      leachdata.LoadFile(@"D:\DK_information\TestData\FileStructure\DaisyLeaching\SoilFarms_dmi10kmgrid_daily1990.txt");
      leachdata.LoadFile(@"D:\DK_information\TestData\FileStructure\DaisyLeaching\SoilFarms_dmi10kmgrid_daily1991.txt");
      leachdata.LoadFile(@"D:\DK_information\TestData\FileStructure\DaisyLeaching\SoilFarms_dmi10kmgrid_daily1992.txt");
      leachdata.LoadFile(@"D:\DK_information\TestData\FileStructure\DaisyLeaching\SoilFarms_dmi10kmgrid_daily1993.txt");
      leachdata.LoadFile(@"D:\DK_information\TestData\FileStructure\DaisyLeaching\SoilFarms_dmi10kmgrid_daily1994.txt");
      leachdata.LoadFile(@"D:\DK_information\TestData\FileStructure\DaisyLeaching\SoilFarms_dmi10kmgrid_daily1995.txt");
      leachdata.LoadFile(@"D:\DK_information\TestData\FileStructure\DaisyLeaching\SoilFarms_dmi10kmgrid_daily1996.txt");
      leachdata.LoadFile(@"D:\DK_information\TestData\FileStructure\DaisyLeaching\SoilFarms_dmi10kmgrid_daily1997.txt");
      leachdata.LoadFile(@"D:\DK_information\TestData\FileStructure\DaisyLeaching\SoilFarms_dmi10kmgrid_daily1998.txt");
      leachdata.LoadFile(@"D:\DK_information\TestData\FileStructure\DaisyLeaching\SoilFarms_dmi10kmgrid_daily1999.txt");
      leachdata.LoadFile(@"D:\DK_information\TestData\FileStructure\DaisyLeaching\SoilFarms_dmi10kmgrid_daily2000.txt");
      leachdata.LoadFile(@"D:\DK_information\TestData\FileStructure\DaisyLeaching\SoilFarms_dmi10kmgrid_daily2001.txt");
      leachdata.LoadFile(@"D:\DK_information\TestData\FileStructure\DaisyLeaching\SoilFarms_dmi10kmgrid_daily2002.txt");
      leachdata.LoadFile(@"D:\DK_information\TestData\FileStructure\DaisyLeaching\SoilFarms_dmi10kmgrid_daily2003.txt");
      leachdata.LoadFile(@"D:\DK_information\TestData\FileStructure\DaisyLeaching\SoilFarms_dmi10kmgrid_daily2004.txt");
      leachdata.LoadFile(@"D:\DK_information\TestData\FileStructure\DaisyLeaching\SoilFarms_dmi10kmgrid_daily2005.txt");
      leachdata.LoadFile(@"D:\DK_information\TestData\FileStructure\DaisyLeaching\SoilFarms_dmi10kmgrid_daily2006.txt");
      leachdata.LoadFile(@"D:\DK_information\TestData\FileStructure\DaisyLeaching\SoilFarms_dmi10kmgrid_daily2007.txt");
      leachdata.LoadFile(@"D:\DK_information\TestData\FileStructure\DaisyLeaching\SoilFarms_dmi10kmgrid_daily2008.txt");
      leachdata.LoadFile(@"D:\DK_information\TestData\FileStructure\DaisyLeaching\SoilFarms_dmi10kmgrid_daily2009.txt");
      leachdata.LoadFile(@"D:\DK_information\TestData\FileStructure\DaisyLeaching\SoilFarms_dmi10kmgrid_daily2010.txt");
      leachdata.LoadFile(@"D:\DK_information\TestData\FileStructure\DaisyLeaching\SoilFarms_dmi10kmgrid_daily2011.txt");



      DateTime Start = new DateTime(1991, 1, 1);
      DateTime End = new DateTime(2012, 1, 1);
      int numberofmonths = (End.Year - Start.Year) * 12 + End.Month - Start.Month;
      Dictionary<int, float[]> CatchLeach = new Dictionary<int, float[]>();

      Parallel.ForEach(dist, c =>
        {
          List<float> values = new List<float>();
          for (int i = 0; i < numberofmonths; i++)
            values.Add(0);

          foreach (var p in c.Value)
          {
            var newlist = leachdata.GetValues(p, Start, End);
            for (int i = 0; i < numberofmonths; i++)
              values[i] += newlist[i];
          }
          lock(Lock)
            CatchLeach.Add(c.Key, values.ToArray());
        });

      using (HydroNumerics.Geometry.Shapes.ShapeWriter sw = new Geometry.Shapes.ShapeWriter(@"D:\DK_information\TestData\leach1990Montly"))
      {
        System.Data.DataTable dt = new System.Data.DataTable();

        dt.Columns.Add("ID15", typeof(int));
        dt.Columns.Add("Januar", typeof(double));
        dt.Columns.Add("Februar", typeof(double));
        dt.Columns.Add("Marts", typeof(double));
        dt.Columns.Add("April", typeof(double));
        dt.Columns.Add("Maj", typeof(double));
        dt.Columns.Add("Juni", typeof(double));
        dt.Columns.Add("Juli", typeof(double));
        dt.Columns.Add("August", typeof(double));
        dt.Columns.Add("September", typeof(double));
        dt.Columns.Add("Oktober", typeof(double));
        dt.Columns.Add("November", typeof(double));
        dt.Columns.Add("December", typeof(double));

        foreach (var c in mv.AllCatchments.Values)
        {
          var dr = dt.NewRow();
          dr[0] = c.ID;
          var data = CatchLeach[c.ID];

          for (int i = 0; i < 12; i++)
            dr[i + 1] = data[i];
          sw.Write(new HydroNumerics.Geometry.GeoRefData() { Geometry = c.Geometry, Data = dr });
        }
      }

      using (HydroNumerics.Geometry.Shapes.ShapeWriter sw = new Geometry.Shapes.ShapeWriter(@"D:\DK_information\TestData\leachYearlyscaled"))
      {
        System.Data.DataTable dt = new System.Data.DataTable();

        dt.Columns.Add("ID15", typeof(int));

        for (int i= Start.Year; i<= End.Year;i++)
        {
          dt.Columns.Add(i.ToString(), typeof(double));
        }

        foreach (var c in mv.AllCatchments.Values)
        {
          var dr = dt.NewRow();
          dr[0] = c.ID;
          var data = CatchLeach[c.ID];

          for (int i = 0; i < End.Year - Start.Year; i++)
            dr[i + 1] = data.Skip(i * 12).Take(12).Sum()/((HydroNumerics.Geometry.IXYPolygon)c.Geometry).GetArea(); 
          sw.Write(new HydroNumerics.Geometry.GeoRefData() { Geometry = c.Geometry, Data = dr });
        }
      }



      double sum = CatchLeach.Values.Sum(c=>c.Sum(v=>v));

    }








    


    [TestMethod]
    public void LoadAndCombineTest()
    {
      MainViewModel mv = new MainViewModel();
      mv.LoadCatchments(@"D:\DK_information\TestData\FileStructure\id15_NSTmodel.shp");

      GroundWaterSource target = new GroundWaterSource();
      target.LoadParticles(@"D:\DK_information\TestData\FileStructure\Particles\PTReg_Extraction_1_20131007_dk2.shp");

      target.LoadSoilCodesGrid(@"D:\DK_information\TestData\FileStructure\DaisyLeaching\DKDomainNodes_LU_Soil_codes.shp");
      target.LoadDaisyData(@"D:\DK_information\TestData\FileStructure\DaisyLeaching\Leaching_area_2.txt");

      target.CombineParticlesAndCatchments(mv.AllCatchments.Values);
      Assert.AreEqual(0, target.Particles.Count(P => P == null));
      Assert.AreEqual(0, mv.AllCatchments.Values.SelectMany(c => c.Particles.Where(P => P == null)).Count());
      Stopwatch sw = new Stopwatch();
      sw.Start();
      target.BuildInputConcentration(new DateTime(1994, 1, 1), new DateTime(2008, 5, 1),mv.AllCatchments.Values, 100);
      sw.Stop();

    }

    
  }
}
