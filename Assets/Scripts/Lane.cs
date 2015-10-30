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

    // Gets the offset for each lane
    public static Vector3 GetOffset(this Lane lane)
    {
        switch (lane)
        {
            case Lane.CLOSE:
                return new Vector3
                (
                    -1.8f,
                    0,
                    0
                );

            case Lane.MID:
                return new Vector3
                (
                    0,
                    0,
                    0
                );
                break;

            case Lane.FAR:
                return new Vector3
                (
                    1.75f,
                    0,
                    0
                );
                break;
        }

        return new Vector3();
    }
}