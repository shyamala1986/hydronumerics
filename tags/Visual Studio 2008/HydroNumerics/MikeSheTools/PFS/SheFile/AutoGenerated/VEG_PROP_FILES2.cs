using System;
using System.Collections.Generic;
using DHI.Generic.MikeZero;

namespace HydroNumerics.MikeSheTools.PFS.SheFile
{
  /// <summary>
  /// This is an autogenerated class. Do not edit. 
  /// If you want to add methods create a new partial class in another file
  /// </summary>
  public partial class VEG_PROP_FILES2: PFSMapper
  {

    private VegNo_16 _vegNo_1;

    internal VEG_PROP_FILES2(PFSSection Section)
    {
      _pfsHandle = Section;

      for (int i = 1; i <= Section.GetSectionsNo(); i++)
      {
        PFSSection sub = Section.GetSection(i);
        switch (sub.Name)
        {
        case "VegNo_1":
          _vegNo_1 = new VegNo_16(sub);
          break;
          default:
            _unMappedSections.Add(sub.Name);
          break;
        }
      }
    }

    public VegNo_16 VegNo_1
    {
     get { return _vegNo_1; }
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

    public int MzSEPfsListItemCount
    {
      get
      {
        return _pfsHandle.GetKeyword("MzSEPfsListItemCount", 1).GetParameter(1).ToInt();
      }
      set
      {
        _pfsHandle.GetKeyword("MzSEPfsListItemCount", 1).GetParameter(1).Value = value;
      }
    }

  }
}
