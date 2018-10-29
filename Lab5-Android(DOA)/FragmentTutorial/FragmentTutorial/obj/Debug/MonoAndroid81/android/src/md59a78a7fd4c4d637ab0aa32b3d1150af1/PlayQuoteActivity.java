package md59a78a7fd4c4d637ab0aa32b3d1150af1;


public class PlayQuoteActivity
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
		mono.android.Runtime.register ("FragmentTutorial.PlayQuoteActivity, FragmentTutorial", PlayQuoteActivity.class, __md_methods);
	}


	public PlayQuoteActivity ()
	{
		super ();
		if (getClass () == PlayQuoteActivity.class)
			mono.android.TypeManager.Activate ("FragmentTutorial.PlayQuoteActivity, FragmentTutorial", "", this, new java.lang.Object[] {  });
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
