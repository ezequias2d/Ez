// Copyright (c) 2021 ezequias2d <ezequiasmoises@gmail.com> and the Ez contributors
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.
using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Ez.IO
{
    public static class PathHelper
    {
        private static readonly char[] PathSeparators = { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar };

        /// <summary>
        /// Split the path by separator.
        /// </summary>
        /// <param name="path">Path.</param>
        /// <returns>Split path.</returns>
        public static string[] SeparatePath(string path)
        {
            return path.Split(PathSeparators, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Get folder name from path.
        /// </summary>
        /// <param name="path">Path</param>
        /// <returns>The name of last folder of path(considers folder up to the last folder separator, example: c:/path/foo = path. c:/path/foo/ = foo.).</returns>
        public static string GetFolderName(string path)
        {
            return Path.GetFileName(Path.GetDirectoryName(path));
        }

        /// <summary>
        /// Create a relative path from one path to another. Paths will be resolved before calculating the difference.
        /// Default path comparison for the active platform will be used (OrdinalIgnoreCase for Windows or Mac, Ordinal for Unix).
        /// </summary>
        /// <param name="relativeTo">The source path the output should be relative to. This path is always considered to be a directory.</param>
        /// <param name="path">The destination path.</param>
        /// <returns>The relative path or <paramref name="path"/> if the paths don't share the same root.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="relativeTo"/> or <paramref name="path"/> is <c>null</c> or an empty string.</exception>
        public static string GetRelativePath(string relativeTo, string path, StringComparison comparisonType = StringComparison.Ordinal)
        {
            if (relativeTo == null)
                throw new ArgumentNullException(nameof(relativeTo));

            if (string.IsNullOrWhiteSpace(relativeTo))
                throw new ArgumentException("Relative path can't be ", nameof(relativeTo));

            if (path == null)
                throw new ArgumentNullException(nameof(path));

            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("Path is empty", nameof(path));

            Debug.Assert(comparisonType == StringComparison.Ordinal || comparisonType == StringComparison.OrdinalIgnoreCase);

            relativeTo = Path.GetFullPath(relativeTo);
            path = Path.GetFullPath(path);

            // Need to check if the roots are different- if they are we need to return the "to" path.
            if (!string.Equals(Path.GetPathRoot(relativeTo), Path.GetPathRoot(path), comparisonType))
                return path;

            string[] relativeToParts = SeparatePath(relativeTo);
            string[] pathParts = SeparatePath(path);

            int commonCount;
            for (
                commonCount = 0;
                commonCount < pathParts.Length &&
                commonCount < relativeToParts.Length &&
                string.Equals(pathParts[commonCount], relativeToParts[commonCount], comparisonType);
                commonCount++)
            {
            }

            // Add .. for the way up from relBase
            StringBuilder newPath = new StringBuilder();
            for (int i = commonCount; i < relativeToParts.Length; i++)
            {
                newPath.Append("..");
                newPath.Append(Path.DirectorySeparatorChar);
            }

            // Append the remaining part of the path
            for (int i = commonCount; i < pathParts.Length; i++)
            {
                newPath.Append(pathParts[i]);

                if (i < pathParts.Length - 1)
                    newPath.Append(Path.DirectorySeparatorChar);
            }

            return newPath.ToString();
        }

    }
}
