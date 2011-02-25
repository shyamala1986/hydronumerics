using System;
using System.Collections.Generic;
using DHI.Generic.MikeZero;

namespace HydroNumerics.MikeSheTools.PFS.SheFile
{
  /// <summary>
  /// This is an autogenerated class. Do not edit. 
  /// If you want to add methods create a new partial class in another file
  /// </summary>
  public partial class Layer_2: PFSMapper
  {

    private Bathymetry _lowerLevel;
    private Bathymetry _initPotHead;
    private Bathymetry _initialSoilTemperature;
    private OuterBoundary _outerBoundary;
    private Topography _hydrHeadUsedForAirFlow;
    private Topography _wettingThreshold;
    private InternalBoundary1 _internalBoundary;
    private InitialMass _initial_Concentration;
    private InitialMass _initial_Immobile_Concentration;

    internal Layer_2(PFSSection Section)
    {
      _pfsHandle = Section;

      for (int i = 1; i <= Section.GetSectionsNo(); i++)
      {
        PFSSection sub = Section.GetSection(i);
        switch (sub.Name)
        {
        case "LowerLevel":
          _lowerLevel = new Bathymetry(sub);
          break;
        case "InitPotHead":
          _initPotHead = new Bathymetry(sub);
          break;
        case "InitialSoilTemperature":
          _initialSoilTemperature = new Bathymetry(sub);
          break;
        case "OuterBoundary":
          _outerBoundary = new OuterBoundary(sub);
          break;
        case "HydrHeadUsedForAirFlow":
          _hydrHeadUsedForAirFlow = new Topography(sub);
          break;
        case "WettingThreshold":
          _wettingThreshold = new Topography(sub);
          break;
        case "InternalBoundary":
          _internalBoundary = new InternalBoundary1(sub);
          break;
        case "Initial_Concentration":
          _initial_Concentration = new InitialMass(sub);
          break;
        case "Initial_Immobile_Concentration":
          _initial_Immobile_Concentration = new InitialMass(sub);
          break;
          default:
            _unMappedSections.Add(sub.Name);
          break;
        }
      }
    }

    public Bathymetry LowerLevel
    {
     get { return _lowerLevel; }
    }

    public Bathymetry InitPotHead
    {
     get { return _initPotHead; }
    }

    public Bathymetry InitialSoilTemperature
    {
     get { return _initialSoilTemperature; }
    }

    public OuterBoundary OuterBoundary
    {
     get { return _outerBoundary; }
    }

    public Topography HydrHeadUsedForAirFlow
    {
     get { return _hydrHeadUsedForAirFlow; }
    }

    public Topography WettingThreshold
    {
     get { return _wettingThreshold; }
    }

    public InternalBoundary1 InternalBoundary
    {
     get { return _internalBoundary; }
    }

    public InitialMass Initial_Concentration
    {
     get { return _initial_Concentration; }
    }

    public InitialMass Initial_Immobile_Concentration
    {
     get { return _initial_Immobile_Concentration; }
    }

    public string Name
    {
      get
      {
        return _pfsHandle.GetKeyword("Name", 1).GetParameter(1).ToString();
      }
      set
      {
        _pfsHandle.GetKeyword("Name", 1).GetParameter(1).Value = value;
      }
    }

    public int ModLayAvg
    {
      get
      {
        return _pfsHandle.GetKeyword("ModLayAvg", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("ModLayAvg", 1).GetParameter(1).Value = value;
      }
    }

    public int ModLayCon
    {
      get
      {
        return _pfsHandle.GetKeyword("ModLayCon", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("ModLayCon", 1).GetParameter(1).Value = value;
      }
    }

    public int id
    {
      get
      {
        return _pfsHandle.GetKeyword("id", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("id", 1).GetParameter(1).Value = value;
      }
    }

  }
}
