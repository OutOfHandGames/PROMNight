using UnityEngine;
using System.Collections;

public interface MapProperties {

    Tile getTile(Point2 point);
    Tile getTile(int x, int y);
}
