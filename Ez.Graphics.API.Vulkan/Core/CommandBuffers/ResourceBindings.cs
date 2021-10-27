using Ez.Graphics.API.Vulkan.Core.Textures;
using System.Collections;
using System.Collections.Generic;

namespace Ez.Graphics.API.Vulkan.Core
{
    internal class ResourceBindings : IEnumerable<BindingInfo>
    {
        private readonly IDictionary<uint, BindingInfo> _bindings;
        private readonly IDictionary<uint, BindingInfo> _temp;
        public ResourceBindings()
        {
            _bindings = new Dictionary<uint, BindingInfo>();
            _temp = new Dictionary<uint, BindingInfo>();
            IsDirty = true;
        }

        public bool IsDirty { get; set; }

        public int Length { get; private set; }

        public void BindBuffer(BufferUsage usage, BufferSpan buffer, uint binding) =>
            Bind(new()
            {
                BufferUsage = usage,
                Buffer = buffer,

                Texture = null,
                Sampler = null,

                Binding = binding,
            });

        public void BindTexture(BaseTexture texture, Sampler sampler, uint binding) =>
            Bind(new()
            {
                Buffer = new()
                {
                    Buffer = null,
                    Offset = 0,
                    Size = 0,
                },
                BufferUsage = BufferUsage.None,

                Texture = texture,
                Sampler = sampler,

                Binding = binding,
            });

        private void Bind(BindingInfo bindingInfo)
        {
            if ((!_bindings.ContainsKey(bindingInfo.Binding) || bindingInfo != _bindings[bindingInfo.Binding]) && bindingInfo.IsBinding)
            {
                _bindings[bindingInfo.Binding] = bindingInfo;
                IsDirty = true;
                Length++;
            }
        }

        public void Reset()
        {
            if (_bindings.Count > 0)
            {
                _bindings.Clear();
                IsDirty = true;
                Length = 0;
            }
        }

        public IEnumerator<BindingInfo> GetEnumerator()
        {
            foreach (var binding in _bindings)
            {
                var value = binding.Value;
                if (value.IsBinding)
                {
                    _temp[binding.Key] = value;
                    yield return value;
                }
            }
            IsDirty = false;

            foreach (var binding in _temp)
                _bindings[binding.Key] = binding.Value;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
