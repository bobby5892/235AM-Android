package md5e68c175ce870c4efa0ba7e0fd13af43b;


public class ComplaintAdapter
	extends android.widget.SimpleAdapter
	implements
		mono.android.IGCUserPeer,
		android.widget.SectionIndexer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getPositionForSection:(I)I:GetGetPositionForSection_IHandler:Android.Widget.ISectionIndexerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_getSectionForPosition:(I)I:GetGetSectionForPosition_IHandler:Android.Widget.ISectionIndexerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_getSections:()[Ljava/lang/Object;:GetGetSectionsHandler:Android.Widget.ISectionIndexerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("ComplaintDepartment.ComplaintAdapter, ComplaintDepartment", ComplaintAdapter.class, __md_methods);
	}


	public ComplaintAdapter (android.content.Context p0, java.util.List p1, int p2, java.lang.String[] p3, int[] p4)
	{
		super (p0, p1, p2, p3, p4);
		if (getClass () == ComplaintAdapter.class)
			mono.android.TypeManager.Activate ("ComplaintDepartment.ComplaintAdapter, ComplaintDepartment", "Android.Content.Context, Mono.Android:System.Collections.Generic.IList`1<System.Collections.Generic.IDictionary`2<System.String,System.Object>>, mscorlib:System.Int32, mscorlib:System.String[], mscorlib:System.Int32[], mscorlib", this, new java.lang.Object[] { p0, p1, p2, p3, p4 });
	}


	public int getPositionForSection (int p0)
	{
		return n_getPositionForSection (p0);
	}

	private native int n_getPositionForSection (int p0);


	public int getSectionForPosition (int p0)
	{
		return n_getSectionForPosition (p0);
	}

	private native int n_getSectionForPosition (int p0);


	public java.lang.Object[] getSections ()
	{
		return n_getSections ();
	}

	private native java.lang.Object[] n_getSections ();

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
