using System.ComponentModel;
using System.Reflection;

namespace ListenAI.Factory.FirmwareDeploy {
    public class Utils {
        public static Control CloneControl(Control srcCtl) {
            //ref: https://stackoverflow.com/questions/3473597/it-is-possible-to-copy-all-the-properties-of-a-certain-control-c-window-forms
            var cloned = Activator.CreateInstance(srcCtl.GetType()) as Control;
            var binding = BindingFlags.Public | BindingFlags.Instance;
            foreach (PropertyInfo prop in srcCtl.GetType().GetProperties(binding)) {
                if (IsClonable(prop)) {
                    object val = prop.GetValue(srcCtl);
                    prop.SetValue(cloned, val, null);
                }
            }

            foreach (Control ctl in srcCtl.Controls) {
                cloned.Controls.Add(CloneControl(ctl));
            }

            return cloned;
        }

        private static bool IsClonable(PropertyInfo prop) {
            if (prop.Name == "Name") {
                return true;
            }
            var browsableAttr = prop.GetCustomAttribute(typeof(BrowsableAttribute), true) as BrowsableAttribute;
            var editorBrowsableAttr = prop.GetCustomAttribute(typeof(EditorBrowsableAttribute), true) as EditorBrowsableAttribute;

            return prop.CanWrite
                && (browsableAttr == null || browsableAttr.Browsable == true)
                && (editorBrowsableAttr == null || editorBrowsableAttr.State != EditorBrowsableState.Advanced);
        }

        public static Control CtrlPropModify(Control ctrl, int id) {
            var binding = BindingFlags.Public | BindingFlags.Instance;

            foreach (PropertyInfo prop in ctrl.GetType().GetProperties(binding)) {
                if (IsClonable(prop)) {
                    object val = prop.GetValue(ctrl);
                    switch (prop.Name) {
                        case "Name":
                            var newName = val.ToString().Replace("1", id.ToString());
                            prop.SetValue(ctrl, newName);

                            if (newName == $"lbGroup{id}Title") {
                                ctrl.Text = $"模组{ConvertToChineseChars(id)}";
                            }
                            break;
                        default:
                            prop.SetValue(ctrl, val, null);
                            break;
                    }
                }
            }

            foreach (Control subCtrl in ctrl.Controls) {
                CtrlPropModify(subCtrl, id);
            }

            return ctrl;
        }

        public static string ConvertToChineseChars(int num) {
            var result = num.ToString();
            result = result.Replace("0", "零")
                .Replace("1", "一")
                .Replace("2", "二")
                .Replace("3", "三")
                .Replace("4", "四")
                .Replace("5", "五")
                .Replace("6", "六")
                .Replace("7", "七")
                .Replace("8", "八")
                .Replace("9", "九");

            return result;
        }
    }
}
