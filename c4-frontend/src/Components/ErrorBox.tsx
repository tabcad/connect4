import styled from "@emotion/styled";
import { Header } from "./Header";

export const ErrorBox = () => {
  return (
    <BoxWrapper>
      <Header bgColor="red">ERROR</Header>
      <p>oh no...</p>
      <p>error message</p>
    </BoxWrapper>
  );
};

const BoxWrapper = styled.div({
  width: "500px",
  height: "280px",
  fontSize: "22px",
  border: "2px solid black",
  borderRadius: "1px",
  boxShadow: "-2px 2px 1px 0 black",
  paddingBottom: "20px",
  marginBottom: '100px',
});


// function handleError(err: Error) {
//   if (isError(err)) {
//     err.response.json().then((p: ProblemResponse) => {
//       if ((p.detail || p.title) && p.requestId) {
//         addToast({
//           id: p.requestId,
//           content: <ToastContent title={p.detail || p.title} icon="error" />,
//           isDismissable: true,
//         });
//       }
//     });
//   }
// }