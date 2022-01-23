using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public static class Extensions
{
    //=========================================================================
    // Removes all instances of [itemToRemove] from array [original]
    // Returns the new array, without modifying [original] directly
    // .Net2.0-compatible
    public static T[] RemoveFromArray<T>(this T[] original, T itemToRemove)
    {
        int numIdx = System.Array.IndexOf(original, itemToRemove);
        if (numIdx == -1) return original;
        List<T> tmp = new List<T>(original);
        tmp.RemoveAt(numIdx);
        return tmp.ToArray();
    }

    //=========================================================================
    // Removes all instances of [itemToRemove] from array [original]
    // Returns the new array, without modifying [original] directly
    // .Net2.0-compatible
    public static T[] RemoveFromArray<T>(this T[] original, int iItemId)
    {
        if (iItemId == -1) return original;
        List<T> tmp = new List<T>(original);
        tmp.RemoveAt(iItemId);
        return tmp.ToArray();
    }

    public static Version IncrementRevision(this Version version)
    {
        return AddVersion(version, 0, 0, 0, 1);
    }
    public static Version IncrementBuild(this Version version)
    {
        return IncrementBuild(version, true);
    }
    public static Version IncrementBuild(this Version version, bool resetLowerNumbers)
    {
        return AddVersion(version, 0, 0, 1, resetLowerNumbers ? -version.Revision : 0);
    }
    public static Version IncrementMinor(this Version version)
    {
        return IncrementMinor(version, true);
    }
    public static Version IncrementMinor(this Version version, bool resetLowerNumbers)
    {
        return AddVersion(version, 0, 1, resetLowerNumbers ? -version.Build : 0, resetLowerNumbers ? -version.Revision : 0);
    }
    public static Version IncrementMajor(this Version version)
    {
        return IncrementMajor(version, true);
    }
    public static Version IncrementMajor(this Version version, bool resetLowerNumbers)
    {
        return AddVersion(version, 1, resetLowerNumbers ? -version.Minor : 0, resetLowerNumbers ? -version.Build : 0, resetLowerNumbers ? -version.Revision : 0);
    }

    public static Version AddVersion(this Version version, string addVersion)
    {
        return AddVersion(version, new Version(addVersion));
    }
    public static Version AddVersion(this Version version, Version addVersion)
    {
        return AddVersion(version, addVersion.Major, addVersion.Minor, addVersion.Build, addVersion.Revision);
    }
    public static Version AddVersion(this Version version, int major, int minor, int build, int revision)
    {
        return SetVersion(version, version.Major + major, version.Minor + minor, version.Build + build, version.Revision + revision);
    }
    public static Version SetVersion(this Version version, int major, int minor, int build, int revision)
    {
        return new Version(major, minor, build, revision);
    }
}



namespace SoftObject.TrainConcept.Libraries
{
    public class Utilities
    {
        public static string c_strDefaultFinalTest = "FinalTest";
        public static void ShiftElement<T>(T[] array, int oldIndex, int newIndex)
        {
            if (oldIndex == newIndex)
            {
                return; // No-op
            }
            T tmp = array[oldIndex];
            if (newIndex < oldIndex)
            {
                // Need to move part of the array "up" to make room
                Array.Copy(array, newIndex, array, newIndex + 1, oldIndex - newIndex);
            }
            else
            {
                // Need to move part of the array "down" to fill the gap
                Array.Copy(array, oldIndex + 1, array, oldIndex, newIndex - oldIndex);
            }
            array[newIndex] = tmp;
        }

        public static bool SplitPath(string path, out string[] aTitles)
        {
            if (path.Length > 0)
            {
                string[] l_aTitles = new string[4];
                aTitles = new string[4];
                int erg = 0;

                int found = path.LastIndexOf("\\\\");
                if (found > 0)
                {
                    l_aTitles[0] = path.Substring(found + 2);
                    erg = 2;
                    path = path.Substring(0, found);
                    found = path.LastIndexOf("\\\\");
                    if (found > 0)
                    {
                        l_aTitles[1] = path.Substring(found + 2);
                        erg = 3;
                        path = path.Substring(0, found);
                        found = path.LastIndexOf("\\\\");
                        if (found > 0)
                        {
                            l_aTitles[2] = path.Substring(found + 2);
                            path = path.Substring(0, found);
                            l_aTitles[3] = path;
                            erg = 4;
                        }
                        else
                        {
                            l_aTitles[2] = path;
                            erg = 3;
                        }

                    }
                    else
                    {
                        l_aTitles[1] = path;
                        erg = 2;
                    }
                }
                else
                {
                    l_aTitles[0] = path;
                    erg = 1;
                }

                switch (erg)
                {
                    case 1: aTitles = new string[1];
                        aTitles[0] = l_aTitles[0];
                        break;
                    case 2: aTitles = new string[2];
                        aTitles[0] = l_aTitles[1];
                        aTitles[1] = l_aTitles[0];
                        break;
                    case 3: aTitles = new string[3];
                        aTitles[0] = l_aTitles[2];
                        aTitles[1] = l_aTitles[1];
                        aTitles[2] = l_aTitles[0];
                        break;
                    case 4: aTitles = new string[4];
                        aTitles[0] = l_aTitles[3];
                        aTitles[1] = l_aTitles[2];
                        aTitles[2] = l_aTitles[1];
                        aTitles[3] = l_aTitles[0];
                        break;
                }
                return true;
            }

            aTitles = new string[1];
            aTitles[0] = "";
            return false;
        }

        public static string MergePath(string[] aTitles)
        {
            string path = aTitles[0];
            for (int i = 1; i < aTitles.Length; ++i)
                path = path + "\\\\" + aTitles[i];
            return path;
        }

        public static bool Str2TestType(string sType, out TestType type)
        {
            if (TestType.TryParse(sType, true, out type))
                return true;
            return false;
        }

        public static string GetTemplateName(string strTemplate)
        {
            int iFound = strTemplate.ToLower().IndexOf("_f.htm");
            if (iFound > 0)
                return strTemplate.Substring(0, iFound);
            return "";
        }

        public static bool GetSolutionFile(string strDocFileName, ref string strSolFileName)
        {
            if (strSolFileName == null)
                throw new ArgumentNullException(nameof(strSolFileName));
            string strDir = Path.GetDirectoryName(strDocFileName);
            string strName = Path.GetFileNameWithoutExtension(strDocFileName);
            string strExt = Path.GetExtension(strDocFileName);
            string strSolutionFileName = strDir + @"\" + strName + "_solution" + strExt;

            strSolFileName = strSolutionFileName;
            return (File.Exists(strSolutionFileName));
        }


        public static bool ParseVersion(string input, ref Version version, ref String strError)
        {
            try
            {
                version = Version.Parse(input);
                return true;
            }
            catch (ArgumentNullException)
            {
                strError = "ArgumentNullException";
            }
            catch (ArgumentOutOfRangeException)
            {
                strError = String.Format("Error: Negative value in '{0}'.", input);
            }
            catch (ArgumentException)
            {
                strError = String.Format("Error: Bad number of components in '{0}'.",
                    input);
            }
            catch (FormatException)
            {
                strError = String.Format("Error: Non-integer value in '{0}'.", input);
            }
            catch (OverflowException)
            {
                strError = String.Format("Error: Number out of range in '{0}'.", input);
            }

            return false;
        }
    }
}
