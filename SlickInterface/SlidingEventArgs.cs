/*

  SlickInterface
  Copyright (c) 2010, Christoph Heindl
  All rights reserved.

  Redistribution and use in source and binary forms, with or without modification, 
  are permitted provided that the following conditions are met:
  - Redistributions of source code must retain the above copyright notice, this list 
    of conditions and the following disclaimer.
  - Redistributions in binary form must reproduce the above copyright notice, this list 
    of conditions and the following disclaimer in the documentation and/or other materials 
    provided with the distribution.
  - Neither the name of the Christoph Heindl nor the names of its contributors may be 
    used to endorse or promote products derived from this software without specific prior 
    written permission.

  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
  ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
  WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
  IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
  INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
  NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, 
  OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
  WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING 
  IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SlickInterface {

  /// <summary>
  /// Defines possible slide movement directions
  /// </summary>
  public enum SlideDirection {
    /// <summary>
    /// When the slide direction is considered a forward movement
    /// </summary>
    Forward = 0,
    /// <summary>
    /// When the slide direction is considered a backward movement
    /// </summary>
    Backward
  }

  /// <summary>
  /// Arguments for forward/backward slide motions provided to affected slides
  /// </summary>
  public class SlidingEventArgs {
    private SlideDirection _slide_direction;
    private object _user_data;

    /// <summary>
    /// Initialize with direction
    /// </summary>
    /// <param name="dir">Direction</param>
    /// <param name="user_data">User data</param>
    public SlidingEventArgs(SlideDirection dir, object user_data) {
      _slide_direction = dir;
      _user_data = user_data;
    }

    /// <summary>
    /// Get the slide direction
    /// </summary>
    public SlideDirection SlideDirection {
      get { return _slide_direction; }
    }

    /// <summary>
    /// Access userdata associated with this slide event
    /// </summary>
    public object UserData {
      get { return _user_data; }
    }
  }
}
