using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;

namespace Contra
{
    /// <summary>
    /// The native libraries the GO build needs beside the game executable.
    ///
    /// They ride along inside the launcher as one deflated zip rather than as eight loose files:
    /// together they are about 21MB, which compresses to under 5MB, and a loose file is one more
    /// thing that can go missing from an install or be left behind at the wrong version.
    ///
    /// Extraction is on demand -- nothing is written unless GO is actually being launched and the
    /// file is absent or a different size.
    /// </summary>
    internal static class GoFiles
    {
        /// <summary>The archive's name inside the assembly, as the csproj embeds it.</summary>
        private const string ResourceName = "Contra.GoDlls.zip";

        /// <summary>
        /// Put the GO libraries next to the game if they are not already there.
        /// Returns false when something could not be written, so the caller can decline to launch
        /// into a half unpacked install rather than letting the game fail on a missing import.
        /// </summary>
        public static bool EnsureExtracted(string targetDirectory, out string error)
        {
            error = null;

            try
            {
                Assembly self = Assembly.GetExecutingAssembly();
                using (Stream raw = self.GetManifestResourceStream(ResourceName))
                {
                    if (raw == null)
                    {
                        error = "The GO libraries are missing from this launcher build.";
                        return false;
                    }

                    using (ZipArchive archive = new ZipArchive(raw, ZipArchiveMode.Read))
                    {
                        foreach (ZipArchiveEntry entry in archive.Entries)
                        {
                            string destination = Path.Combine(targetDirectory, entry.Name);

                            // Length is enough to tell a stale or truncated copy from the right one,
                            // and costs nothing next to hashing 21MB on every launch.
                            if (File.Exists(destination) && new FileInfo(destination).Length == entry.Length)
                            {
                                continue;
                            }

                            if (File.Exists(destination))
                            {
                                File.SetAttributes(destination, FileAttributes.Normal);
                            }

                            entry.ExtractToFile(destination, true);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                // A DLL already loaded by a running game cannot be replaced, which is the likeliest
                // failure here and worth naming rather than swallowing.
                error = ex.Message;
                return false;
            }
        }
    }
}
