using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Tekla.Structures;
using Tekla.Structures.Catalogs;
using Tekla.Structures.Model;

namespace TeklaSubComponentAttributesList
{
    public class Tools
    {
    private static string GetAttributeFileExtension(string componentNameOrNumber)
    {
      int result = 0;
      bool flag = int.TryParse(componentNameOrNumber, out result);
      ComponentItemEnumerator componentItems = new Tekla.Structures.Catalogs.CatalogHandler().GetComponentItems();
      while (componentItems.MoveNext())
      {
        ComponentItem current = componentItems.Current;
        if (current.Name == componentNameOrNumber || flag && result == current.Number)
          return current.AttributeFileExtension;
      }
      return string.Empty;
    }

    public static IList<string> GetAttributeFiles(string plugin)
    {
      ModelInfo info = new Tekla.Structures.Model.Model().GetInfo();
      TeklaStructuresFiles teklaStructuresFiles = new TeklaStructuresFiles(info != null ? info.ModelPath : string.Empty);
      if (info != null)
        teklaStructuresFiles.PropertyFileDirectories.Insert(0, info.ModelPath);
      return (IList<string>) teklaStructuresFiles.GetMultiDirectoryFileList(plugin);
    }

    public static void FillDropBoxWithAttributeFiles(ComboBox attributeComboBox, string pluginTag)
    {
      string str1 = attributeComboBox.Text;
      attributeComboBox.Items.Clear();
      if (string.IsNullOrEmpty(pluginTag))
      {
        attributeComboBox.Text = string.Empty;
      }
      else
      {
        try
        {
          IList<string> attributeFiles = GetAttributeFiles(GetAttributeFileExtension(pluginTag));
          foreach (string str2 in (IEnumerable<string>) attributeFiles)
          {
            attributeComboBox.Items.Add((object) str2);
            if (str1 != null && str1 == str2)
              str1 = (string) null;
          }
          if (str1 == null)
            return;
          attributeComboBox.Text = attributeFiles[0];
        }
        catch
        {
        }
      }
    }

    }
}
