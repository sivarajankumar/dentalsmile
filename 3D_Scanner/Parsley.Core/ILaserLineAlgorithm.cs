﻿/*
 * Parsley http://parsley.googlecode.com
 * Copyright (c) 2010, Christoph Heindl. All rights reserved.
 * Code license:	New BSD License
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Emgu.CV.Structure;
using System.ComponentModel;

namespace Parsley.Core {

  /// <summary>
  /// Base class for extraction of laser lines.
  /// </summary>
  /// <remarks>
  /// Laser line is extracted in each x-column of a single channel byte image.
  /// The result is a collection of floating point values that mark the location
  /// of the laser line per x-pixel. When locations with a value greater than
  /// zero are observed, the laser line was found for that x-pixel.
  /// </remarks>
  public interface ILaserLineAlgorithm {

    /// <summary>
    /// Find laser line.
    /// </summary>
    bool FindLaserLine(Bundle bundle);
  }
}
