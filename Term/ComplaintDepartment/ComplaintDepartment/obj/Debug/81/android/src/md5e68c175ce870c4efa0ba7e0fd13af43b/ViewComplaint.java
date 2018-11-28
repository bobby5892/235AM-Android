package md5e68c175ce870c4efa0ba7e0fd13af43b;


public class ViewComplaint
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("ComplaintDepartment.ViewComplaint, ComplaintDepartment", ViewComplaint.class, __md_methods);
	}


	public ViewComplaint ()
	{
		super ();
		if (getClass () == ViewComplaint.class)
			mono.android.TypeManager.Activate ("ComplaintDepartment.ViewComplaint, ComplaintDepartment", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
