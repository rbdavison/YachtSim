using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EyeInTheSky
{
    public class FvCallback : fvw.ICallback
    {
        public void GetHelpText(int layer_handle, int object_handle, ref string help_text)
        {
        }

        public int GetInfoText(int layer_handle, int object_handle, ref string title_bar_txt, ref string dialog_txt)
        {
            return 0;
        }

        public void GetMenuItems(int layer_handle, int object_handle, ref string menu_text)
        {
        }

        public void GetTimeSpan(int layer_handle, ref DateTime begin, ref DateTime end)
        {
        }

        public void GetToolTip(int layer_handle, int object_handle, ref string tool_tip)
        {
        }

        public void OnDoubleClicked(int layer_handle, int object_handle, int fvw_parent_hWnd, double lat, double lon)
        {
        }

        public void OnFalconViewExit(int layer_handle)
        {
        }

        public void OnGeoCircleBounds(int click_id, double lat, double lon, double radius)
        {
        }

        public void OnGeoCircleBoundsCanceled(int click_id)
        {
        }

        public void OnGeoRectBounds(int click_id, double NW_lat, double NW_lon, double SE_lat, double SE_lon)
        {
        }

        public void OnGeoRectBoundsCanceled(int click_id)
        {
        }

        public void OnMouseClick(int click_id, double latitude, double longitude)
        {
        }

        public void OnMouseClickCanceled(int click_id)
        {
        }

        public void OnOverlayClose(int layer_handle)
        {
        }

        public void OnPreClose(int layer_handle, ref int cancel)
        {
            cancel = -1;
        }

        public int OnSelected(int layer_handle, int object_handle, int fv_parent_hWnd, double latitude, double longitude)
        {
            return 1;
        }

        public void OnSnapToInfo(int click_id, double lat, double lon, int point_type, string key_text)
        {
        }

        public void OnSnapToInfoCanceled(int click_id)
        {
        }

        public void OnToolbarButtonPressed(int toolbar_id, int button_number)
        {
        }

        public void SetCurrentViewTime(int layer_handle, DateTime date)
        {
        }
    }
}
