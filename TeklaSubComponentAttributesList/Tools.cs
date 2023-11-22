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
    public static string ConvertBetweenComponentNameAndCustomComponentName(
      Enums.ComponentOrCustomComponent convertFrom,
      string componentOrCustomComponentName)
    {
      string str = string.Empty;
      try
      {
        switch (convertFrom)
        {
          case Enums.ComponentOrCustomComponent.Component:
            str = !new ComponentItem().Select(componentOrCustomComponentName, -1) ? "EB_" + componentOrCustomComponentName : componentOrCustomComponentName;
            break;
          case Enums.ComponentOrCustomComponent.CustomComponent:
            if (componentOrCustomComponentName.StartsWith("EB_"))
            {
              int length = "EB_".Length;
              str = componentOrCustomComponentName.Substring(length);
              break;
            }
            str = componentOrCustomComponentName;
            break;
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine((object) ex);
      }
      return str;
    }


    public static bool GetIndexOfSelectedShoeComponent(
      ref int indexOfSelectedComponent,
      string componentName,
      XMLTreeData xmlTree)
    {
      bool selectedShoeComponent = true;
      try
      {
        string str1 = Tools.ConvertBetweenComponentNameAndCustomComponentName(Enums.ComponentOrCustomComponent.Component, componentName);
        for (int index = 0; index < xmlTree.ColumnShoeDataXML.Count; ++index)
        {
          if (xmlTree.ColumnShoeDataXML[index].ComponentName == str1)
          {
            indexOfSelectedComponent = index;
            break;
          }
          if (xmlTree.ColumnShoeDataXML[index].ComponentShoeType != string.Empty)
          {
            string str2 = xmlTree.ColumnShoeDataXML[index].ComponentShoeType + "_" + (object) xmlTree.ColumnShoeDataXML[index].ComponentShoeSize;
            if (str2 == str1)
            {
              indexOfSelectedComponent = index;
              break;
            }
            if (str2 == componentName)
            {
              indexOfSelectedComponent = index;
              break;
            }
          }
        }
      }
      catch
      {
        selectedShoeComponent = false;
      }
      return selectedShoeComponent;
    }

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

    private void FillCustomComponentCombobox(
      ComboBox comboBox,
      List<string> commonComponentData,
      bool userOpenedPluginFromModel)
    {
      try
      {
        string text = comboBox.Text;
        string str1 = string.Empty;
        if (userOpenedPluginFromModel)
        {
          try
          {
            //str1 = this.GetAttributeValue<string>(this.structuresExtender.GetAttributeName((Control) comboBox));
            str1= comboBox.Text;
          }
          catch (Exception ex)
          {
            Console.WriteLine((object) ex);
          }
        }
        comboBox.Items.Clear();
        comboBox.ItemsSource = commonComponentData;
        if (str1 != string.Empty)
        {
          int num = -1;
          for (int index = 0; index < comboBox.Items.Count; ++index)
          {
            if (comboBox.Items[index].ToString() == str1)
              num = index;
          }
          if (num == -1)
            comboBox.SelectedIndex = 0;
          else
            comboBox.SelectedIndex = num;
        }
        else
        {
          int indexOfSelectedComponent = -1;
          Tools.GetIndexOfSelectedShoeComponent(ref indexOfSelectedComponent, text, XmlTree.XMLTreeData);
          comboBox.SelectedIndex = indexOfSelectedComponent;
        }
        string str2 = comboBox.Items[comboBox.SelectedIndex].ToString();
        this.SetAttributeValue((Control) comboBox, (object) str2);
      }
      catch (Exception ex)
      {
        Console.WriteLine((object) ex);
      }
    }
    }

    public class Enums
    {
        public enum ComponentOrCustomComponent
        {
          Component,
          CustomComponent,
        }


        public enum DialogInputTypeEnum
        {
            NoAutoAttributeFile,
            UseAutoAttributeFile,
        }

    }
}
