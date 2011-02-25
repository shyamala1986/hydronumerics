using System;
using System.Collections.Generic;
using DHI.Generic.MikeZero;

namespace HydroNumerics.MikeSheTools.PFS.SheFile
{
  /// <summary>
  /// This is an autogenerated class. Do not edit. 
  /// If you want to add methods create a new partial class in another file
  /// </summary>
  public partial class Bathymetry: PFSMapper
  {

    private DFS_2D_DATA_FILE _dFS_2D_DATA_FILE;
    private SHAPE_FILE _sHAPE_FILE;
    private XYZ_FILE _xYZ_FILE;

    internal Bathymetry(PFSSection Section)
    {
      _pfsHandle = Section;

      for (int i = 1; i <= Section.GetSectionsNo(); i++)
      {
        PFSSection sub = Section.GetSection(i);
        switch (sub.Name)
        {
        case "DFS_2D_DATA_FILE":
          _dFS_2D_DATA_FILE = new DFS_2D_DATA_FILE(sub);
          break;
        case "SHAPE_FILE":
          _sHAPE_FILE = new SHAPE_FILE(sub);
          break;
        case "XYZ_FILE":
          _xYZ_FILE = new XYZ_FILE(sub);
          break;
          default:
            _unMappedSections.Add(sub.Name);
          break;
        }
      }
    }

    public DFS_2D_DATA_FILE DFS_2D_DATA_FILE
    {
     get { return _dFS_2D_DATA_FILE; }
    }

    public SHAPE_FILE SHAPE_FILE
    {
     get { return _sHAPE_FILE; }
    }

    public XYZ_FILE XYZ_FILE
    {
     get { return _xYZ_FILE; }
    }

    public int Touched
    {
      get
      {
        return _pfsHandle.GetKeyword("Touched", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("Touched", 1).GetParameter(1).Value = value;
      }
    }

    public int IsDataUsedInSetup
    {
      get
      {
        return _pfsHandle.GetKeyword("IsDataUsedInSetup", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("IsDataUsedInSetup", 1).GetParameter(1).Value = value;
      }
    }

    public int Type
    {
      get
      {
        return _pfsHandle.GetKeyword("Type", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("Type", 1).GetParameter(1).Value = value;
      }
    }

    public int FixedValue
    {
      get
      {
        return _pfsHandle.GetKeyword("FixedValue", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("FixedValue", 1).GetParameter(1).Value = value;
      }
    }

    public int InterpMethod
    {
      get
      {
        return _pfsHandle.GetKeyword("InterpMethod", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("InterpMethod", 1).GetParameter(1).Value = value;
      }
    }

    public int SearchRadius
    {
      get
      {
        return _pfsHandle.GetKeyword("SearchRadius", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("SearchRadius", 1).GetParameter(1).Value = value;
      }
    }

    public int ShowGridData
    {
      get
      {
        return _pfsHandle.GetKeyword("ShowGridData", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("ShowGridData", 1).GetParameter(1).Value = value;
      }
    }

    public int ShowShapeData
    {
      get
      {
        return _pfsHandle.GetKeyword("ShowShapeData", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("ShowShapeData", 1).GetParameter(1).Value = value;
      }
    }

    public int ShapeAxisUnit
    {
      get
      {
        return _pfsHandle.GetKeyword("ShapeAxisUnit", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("ShapeAxisUnit", 1).GetParameter(1).Value = value;
      }
    }

    public int ShapeItemUnit
    {
      get
      {
        return _pfsHandle.GetKeyword("ShapeItemUnit", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("ShapeItemUnit", 1).GetParameter(1).Value = value;
      }
    }

    public int RelativeToGround
    {
      get
      {
        return _pfsHandle.GetKeyword("RelativeToGround", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("RelativeToGround", 1).GetParameter(1).Value = value;
      }
    }

  }
}
