/*
 * Parsley http://parsley.googlecode.com
 * Copyright (c) 2010, Christoph Heindl. All rights reserved.
 * Code license:	New BSD License
 */

#pragma once

#include <draw3d/auto_ptr_osg.h>
#include <osg/Node>

namespace Parsley {
  namespace Draw3D {    

    public ref class Node {
    public:
      Node() : _osg(new osg::Node()) {}
      Node(osg::Node *native) : _osg(native) {}
      auto_ptr_osg<osg::Node> node() { return _osg; }

      static Node^ ReadFromFile(System::String ^filepath);

    private:
      auto_ptr_osg<osg::Node> _osg;
    };

    template<class OSGType> 
    public ref class NodeT : public Node {
    public:
      typedef NodeT<OSGType> self_type;

      NodeT() : Node(new OSGType()) {}

      auto_ptr_osg<OSGType> osg() {
        return auto_ptr_osg<OSGType>(
          dynamic_cast<OSGType*>(this->node().get())
        );
      }
    };


  }
}