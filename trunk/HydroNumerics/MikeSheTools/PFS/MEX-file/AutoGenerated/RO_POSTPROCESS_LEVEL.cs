using System;
using System.Collections.Generic;
using DHI.Generic.MikeZero;
using HydroNumerics.MikeSheTools.PFS.SheFile;

namespace HydroNumerics.MikeSheTools.PFS.MEX
{
  /// <summary>
  /// This is an autogenerated class. Do not edit. 
  /// If you want to add methods create a new partial class in another file
  /// </summary>
  public partial class RO_POSTPROCESS_LEVEL: PFSMapper
  {

    private START_CRITERION _sTART_CRITERION;
    private STOP_CRITERION _sTOP_CRITERION;

    internal RO_POSTPROCESS_LEVEL(PFSSection Section)
    {
      _pfsHandle = Section;

      for (int i = 1; i <= Section.GetSectionsNo(); i++)
      {
        PFSSection sub = Section.GetSection(i);
        switch (sub.Name)
        {
        case "START_CRITERION":
          _sTART_CRITERION = new START_CRITERION(sub);
          break;
        case "STOP_CRITERION":
          _sTOP_CRITERION = new STOP_CRITERION(sub);
          break;
          default:
            _unMappedSections.Add(sub.Name);
          break;
        }
      }
    }

    public START_CRITERION START_CRITERION
    {
     get { return _sTART_CRITERION; }
    }

    public STOP_CRITERION STOP_CRITERION
    {
     get { return _sTOP_CRITERION; }
    }

  }
}
