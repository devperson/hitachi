﻿using System;
using System.Reflection;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HitachiDemo.Controls
{
    public class TemplateSelector : BindableObject
    {
        /// <summary>
        /// Property definition for the <see cref="Templates"/> Bindable Property
        /// </summary>
        public static BindableProperty TemplatesProperty = BindableProperty.Create<TemplateSelector, DataTemplateCollection>(x => x.Templates, default(DataTemplateCollection), BindingMode.OneWay, null, TemplatesChanged);
        /// <summary>
        /// Property definition for the <see cref="SelectorFunction"/> Bindable Property
        /// </summary>
        public static BindableProperty SelectorFunctionProperty = BindableProperty.Create<TemplateSelector, Func<Type, DataTemplate>>(x => x.SelectorFunction, null);
        /// <summary>
        /// Property definition for the <see cref="ExceptionOnNoMatch"/> Bindable Property
        /// </summary>
        public static BindableProperty ExceptionOnNoMatchProperty = BindableProperty.Create<TemplateSelector, bool>(x => x.ExceptionOnNoMatch, true);

        /// <summary>
        /// Initialize the TemplateCollections so that each 
        /// instance gets it's own collection
        /// </summary>
        public TemplateSelector()
        {
            Templates = new DataTemplateCollection();
        }
        /// <summary>
        ///  Clears the cache when the set of templates change
        /// </summary>
        /// <param name="bo"></param>
        /// <param name="oldval"></param>
        /// <param name="newval"></param>
        public static void TemplatesChanged(BindableObject bo, DataTemplateCollection oldval, DataTemplateCollection newval)
        {
            var ts = bo as TemplateSelector;
            if (ts == null) return;
            if (oldval != null) oldval.CollectionChanged -= ts.TemplateSetChanged;
            newval.CollectionChanged += ts.TemplateSetChanged;
            ts.Cache = null;
        }

        /// <summary>
        /// Clear the cache on any template set change
        /// If needed this could be optimized to care about the specific
        /// change but I doubt it would be worthwhile.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TemplateSetChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            Cache = null;
        }

        /// <summary>
        /// Private cache of matched types with datatemplates
        /// The cache is reset on any change to <see cref="Templates"/>
        /// </summary>
        private Dictionary<Type, DataTemplate> Cache { get; set; }

        /// <summary>
        /// Bindable property that allows the user to 
        /// determine if a <see cref="NoDataTemplateMatchException"/> is thrown when 
        /// there is no matching template found
        /// </summary>
        public bool ExceptionOnNoMatch
        {
            get { return (bool)GetValue(ExceptionOnNoMatchProperty); }
            set { SetValue(ExceptionOnNoMatchProperty, value); }
        }
        /// <summary>
        /// The collection of DataTemplates
        /// </summary>
        public DataTemplateCollection Templates
        {
            get { return (DataTemplateCollection)GetValue(TemplatesProperty); }
            set { SetValue(TemplatesProperty, value); }
        }

        /// <summary>
        /// A user supplied function of type
        /// <code>Func<typeparamname name="Type"></typeparamname>,<typeparamname name="DataTemplate"></typeparamname></code>
        /// If this function has been supplied it is always called first in the match 
        /// process.
        /// </summary>
        public Func<Type, DataTemplate> SelectorFunction
        {
            get { return (Func<Type, DataTemplate>)GetValue(SelectorFunctionProperty); }
            set { SetValue(SelectorFunctionProperty, value); }
        }


        /// <summary>
        /// Matches a type with a datatemplate
        /// Order of matching=>
        ///     SelectorFunction, 
        ///     Cache, 
        ///     SpecificTypeMatch,
        ///     InterfaceMatch,
        ///     BaseTypeMatch 
        ///     DefaultTempalte
        /// </summary>
        /// <param name="type">Type object type that needs a datatemplate</param>
        /// <returns>The DataTemplate from the WrappedDataTemplates Collection that closest matches 
        /// the type paramater.</returns>
        /// <exception cref="NoDataTemplateMatchException"></exception>Thrown if there is no datatemplate that matches the supplied type
        public DataTemplate TemplateFor(Type type)
        {
            var typesExamined = new List<Type>();
            var template = TemplateForImpl(type, typesExamined);
            if (template == null && ExceptionOnNoMatch)
                throw new Exception("type, typesExamined");
            return template;
        }

        /// <summary>
        /// Interal implementation of <see cref="TemplateFor"/>.
        /// </summary>
        /// <param name="type">The type to match on</param>
        /// <param name="examined">A list of all types examined during the matching process</param>
        /// <returns>A DataTemplate or null</returns>
        private DataTemplate TemplateForImpl(Type type, List<Type> examined)
        {
            if (type == null) return null;//This can happen when we recusively check base types (object.BaseType==null)
            examined.Add(type);
            System.Diagnostics.Contracts.Contract.Assert(Templates != null, "Templates cannot be null");

            Cache = Cache ?? new Dictionary<Type, DataTemplate>();
            DataTemplate retTemplate = null;

            //Prefer the selector function if present
            //This has been moved before the cache check so that
            //the user supplied function has an opportunity to 
            //Make a decision with more information than simply
            //the requested type (perhaps the Ux or Network states...)
            if (SelectorFunction != null) retTemplate = SelectorFunction(type);

            //Happy case we already have the type in our cache
            if (Cache.ContainsKey(type)) return Cache[type];


            //check our list
            retTemplate = Templates.Where(x => x.Type == type).Select(x => x.WrappedTemplate).FirstOrDefault();
            //Check for interfaces
            retTemplate = retTemplate ?? type.GetTypeInfo().ImplementedInterfaces.Select(x => TemplateForImpl(x, examined)).FirstOrDefault();
            //look at base types
            retTemplate = retTemplate ?? TemplateForImpl(type.GetTypeInfo().BaseType, examined);
            //If all else fails try to find a Default Template
            retTemplate = retTemplate ?? Templates.Where(x => x.IsDefault).Select(x => x.WrappedTemplate).FirstOrDefault();

            Cache[type] = retTemplate;
            return retTemplate;
        }

        /// <summary>
        /// Finds a template for the type of the passed in item (<code>item.GetType()</code>)
        /// and creates the content and sets the Binding context of the View
        /// Currently the root of the DataTemplate must be a ViewCell.
        /// </summary>
        /// <param name="item">The item to instantiate a DataTemplate for</param>
        /// <returns>a View with it's binding context set</returns>
        /// <exception cref="InvalidVisualObjectException"></exception>Thrown when the matched datatemplate inflates to an object not derived from either 
        /// <see cref="Xamarin.Forms.View"/> or <see cref="Xamarin.Forms.ViewCell"/>
        public View ViewFor(object item)
        {
            var template = TemplateFor(item.GetType());
            var content = template.CreateContent();
            if (!(content is View) && !(content is ViewCell))
                throw new Exception(content.GetType().ToString());

            var view = (content is View) ? content as View : ((ViewCell)content).View;
            view.BindingContext = item;
            return view;
        }
    }

    public class TemplateContentView<T> : ContentView
    {
        #region Bindable Properties
        /// <summary>
        /// Property definition for the <see cref="TemplateSelector"/> Property
        /// </summary>
        public static readonly BindableProperty TemplateSelectorProperty = BindableProperty.Create<TemplateContentView<T>, TemplateSelector>(x => x.TemplateSelector, default(TemplateSelector));
        /// <summary>
        /// Property definition for the <see cref="ViewModel"/> Property
        /// </summary>
        public static readonly BindableProperty ViewModelProperty = BindableProperty.Create<TemplateContentView<T>, T>(x => x.ViewModel, default(T), BindingMode.OneWay, null, ViewModelChanged);

        /// <summary>
        /// Used to match a type with a datatemplate
        /// <see cref="TemplateSelector"/>
        /// </summary>
        public TemplateSelector TemplateSelector
        {
            get { return (TemplateSelector)GetValue(TemplateSelectorProperty); }
            set { SetValue(TemplateSelectorProperty, value); }
        }

        /// <summary>
        /// There is an argument to use 'object' rather than T
        /// however you can specify T as object.  In addition
        /// T allows the use of marker interfaces to enable
        /// things like Ux Widgets while maintaining 
        /// some typesafety
        /// </summary>
        public T ViewModel
        {
            get { return (T)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        /// <summary>
        /// Call down to the actual controls Implmentation
        /// <see cref="ViewModelChangedImpl"/>
        /// </summary>
        /// <param name="bindable">The TemplateContentView<typeparam name="T"></typeparam></param>
        /// <param name="oldValue">Ignored</param>
        /// <param name="newValue">Passed down to <see cref="ViewModelChangedImpl"/></param>
        /// <exception cref="InvalidBindableException"></exception>Thrown if bindable is not in fact a TemplateContentView<typeparam name="T"></typeparam>
        private static void ViewModelChanged(BindableObject bindable, T oldValue, T newValue)
        {
            var layout = bindable as TemplateContentView<T>;
            if (layout == null)
                throw new Exception("InvalidBindableException");
            layout.ViewModelChangedImpl(newValue);
        }
        #endregion

        /// <summary>
        /// Clears the old Children
        /// Creates the new View and adds it to the Children, and Invalidates the Layout
        /// </summary>
        /// <param name="newvalue"></param>
        private void ViewModelChangedImpl(T newvalue)
        {
            var newchild = TemplateSelector.ViewFor(newvalue);
            //Verify that newchild is a contentview
            Content = newchild;
            InvalidateLayout();
        }
    }

    /// <summary>
    /// Interface to enable DataTemplateCollection to hold
    /// typesafe instances of DataTemplateWrapper
    /// </summary>
    public interface IDataTemplateWrapper
    {
        bool IsDefault { get; set; }
        DataTemplate WrappedTemplate { get; set; }
        Type Type { get; }
    }
    /// <summary>
    /// Wrapper for a DataTemplate.
    /// Unfortunately the default constructor for DataTemplate is internal
    /// so I had to wrap the DataTemplate instead of inheriting it.
    /// </summary>
    /// <typeparam name="T">The object type that this DataTemplateWrapper matches</typeparam>
    public class DataTemplateWrapper<T> : BindableObject, IDataTemplateWrapper
    {
        public static readonly BindableProperty WrappedTemplateProperty = BindableProperty.Create<DataTemplateWrapper<T>, DataTemplate>(x => x.WrappedTemplate, null);
        public static readonly BindableProperty IsDefaultProperty = BindableProperty.Create<DataTemplateWrapper<T>, bool>(x => x.IsDefault, false);

        public bool IsDefault
        {
            get { return (bool)GetValue(IsDefaultProperty); }
            set { SetValue(IsDefaultProperty, value); }
        }
        public DataTemplate WrappedTemplate
        {
            get { return (DataTemplate)GetValue(WrappedTemplateProperty); }
            set { SetValue(WrappedTemplateProperty, value); }
        }

        public Type Type
        {
            get { return typeof(T); }
        }
    }

    /// <summary>
    /// Collection class of IDataTemplateWrapper
    /// Enables xaml definitions of collections.
    /// </summary>
    public class DataTemplateCollection : ObservableCollection<IDataTemplateWrapper> { }
}
