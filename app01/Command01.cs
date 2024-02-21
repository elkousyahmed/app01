using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace app01
{
    public class Command01 : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            var uidoc = commandData.Application.ActiveUIDocument;
            var doc = uidoc.Document;

            Transaction t = new Transaction(doc);
            t.Start("do somthing");

            Level lv = Level.Create(doc, 100);
            lv.Name = "new levelll";
            
            FilteredElementCollector collector1 = new FilteredElementCollector(doc);
            collector1.OfClass(typeof(ViewFamilyType));

            ViewFamilyType floorplan = null;
            foreach (ViewFamilyType curvft in collector1)
            {
                if (curvft.ViewFamily == ViewFamily.FloorPlan)
                {
                    floorplan = curvft;
                    break;
                }
            }

            ViewPlan newplan = ViewPlan.Create(doc, floorplan.Id, lv.Id);
            newplan.Name = " floor plan ";

            FilteredElementCollector collector2 = new FilteredElementCollector(doc);
            collector2.OfCategory(BuiltInCategory.OST_TitleBlocks);

            ViewSheet nsheet = ViewSheet.Create(doc,collector2.FirstElementId());

            t.Commit();

            return Result.Succeeded;


        }
    }
}
