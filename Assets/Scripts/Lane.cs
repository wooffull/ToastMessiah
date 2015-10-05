using UnityEngine;

public enum Lane
{
    FAR,
    MID,
    CLOSE
}

public static class LaneExtensions
{
    // Gets the Y-coordinate matching up with each lane
    public static float GetY(this Lane lane)
    {
        GameObject foundLane = null;

        switch (lane)
        {
            case Lane.CLOSE:
                foundLane = GameObject.Find("LaneMarker(Close)");
                break;

            case Lane.MID:
                foundLane = GameObject.Find("LaneMarker(Middle)");
                break;

            case Lane.FAR:
                foundLane = GameObject.Find("LaneMarker(Far)");
                break;
        }

        if (foundLane != null)
        {
            return foundLane.gameObject.GetComponent<Transform>().position.y;
        }
        else
        {
            return 0;
        }
    }
}