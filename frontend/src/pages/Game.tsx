/** @jsxImportSource @emotion/react */

const PLACES = Array(42).fill(null);

export default function Game() {
  return (
    <div
      css={{
        margin: "auto",
        backgroundColor: "blue",
        width: 700,
        height: 600,
        display: "grid",
        gridTemplateColumns: "repeat(7, 1fr)",
        borderRadius: 5
      }}
    >
      {PLACES.map((circle, i) => (
        <div
          css={{
            width: 90,
            height: 90,
            borderRadius: 100,
            backgroundColor: "white",
            margin: "auto",
          }}
          key={i}
        >
          {circle}
        </div>
      ))}
    </div>
  );
}
