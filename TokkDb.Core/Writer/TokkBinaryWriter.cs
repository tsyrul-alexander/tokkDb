using System.Text;
using TokkDb.Core.Buffer;

namespace TokkDb.Core.Writer;

public class TokkBinaryWriter {
  public TokkBuffer Buffer { get; }
  public int Position { get; set; }

  public TokkBinaryWriter(TokkBuffer buffer) {
    Buffer = buffer;
  }
  
  public void WriteByte(byte value) {
    Buffer.WriteByte(value, Position);
    MovePosition(1);
  }
  
  public void WriteInt(int value) {
    var bytes = BitConverter.GetBytes(value);
    WriteBytes(bytes);
  }
  
  public void WriteBytes(byte[] values) {
    for (var i = 0; i < values.Length; i++) {
      var value = values[i];
      Buffer.WriteByte(value, Position + i);
    }
    MovePosition(values.Length);
  }
  
  public void WriteString(string value) {
    var bytes = Encoding.UTF8.GetBytes(value);
    WriteInt(bytes.Length);
    WriteBytes(bytes);
  }
  
  protected virtual void MovePosition(int count) {
    Position += count;
  }
}
